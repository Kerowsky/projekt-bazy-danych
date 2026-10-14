using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using projekt_bazy_danych.Common;
using projekt_bazy_danych.Common.Enums;
using projekt_bazy_danych.Common.Providers;
using projekt_bazy_danych.Domain.DTOs;
using System.Collections.ObjectModel;
using System.Windows;

namespace projekt_bazy_danych.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        #region Fields
        private readonly DataProvider dataProvider;

        private readonly DodajZamowienieViewModel childVM;
        #endregion

        #region Properites
        public ObservableCollection<PojazdDTO> Pojazdy { get; set; } = [];

        public ObservableCollection<string> Fabryki { get; } = [];

        [ObservableProperty]
        public string? _selectedFactory;
        #endregion
        public MainWindowViewModel(DodajZamowienieViewModel childVM, DataProvider dataProvider)
        {
            this.childVM = childVM;
            this.dataProvider = dataProvider;

            foreach (var name in Enum.GetNames<FactoryIdentity>())
                Fabryki.Add(name);
        }

        #region Commands
        [RelayCommand]
        private Task Szukaj() => ZaladujDaneDoTabeliAsync();

        [RelayCommand]
        private void Dodaj() 
        {
            var test = new DodajZamowienieWindow(this.childVM);

            test.Owner = Application.Current.MainWindow;
            test.ShowDialog();
        }

        [RelayCommand]
        private void PokazWykres()
        {
            var window = new ChartWindow();
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }
        #endregion

        #region Events
        /// <summary>
        /// Event wywoływany w momencie zmiany comboboxa FABRYKA
        /// </summary>
        /// <param name="value"></param>
        partial void OnSelectedFactoryChanged(string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _ = ZaladujDaneDoTabeliAsync();
            }
        }
        #endregion

        #region Methods
        private async Task ZaladujDaneDoTabeliAsync()
        {
            Pojazdy.Clear();
            try
            {
                ObservableCollection<PojazdDTO> pojazdyDTO = await this.dataProvider.GetPojazdy(SelectedFactory ?? string.Empty);
                foreach(var e in pojazdyDTO)
                {
                    Pojazdy.Add(e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas ładowania danych:\n{ex.Message}",
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}
