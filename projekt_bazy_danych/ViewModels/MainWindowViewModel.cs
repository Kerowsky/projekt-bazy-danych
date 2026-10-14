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
        private Task Szukaj() => ZaladujAsync();
        #endregion

        #region Methods
        private async Task ZaladujAsync()
        {
            try
            {
                var test = await this.dataProvider.GetPojazdy(SelectedFactory ?? string.Empty);
                foreach(var e in test)
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
