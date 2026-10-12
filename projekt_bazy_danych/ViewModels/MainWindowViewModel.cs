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
        private readonly ViewDataProvider dataProvider;

        private readonly DodajZamowienieViewModel childVM;

        [ObservableProperty]
        private string _filterNazwisko = string.Empty;

        [ObservableProperty]
        private string _filterVin = string.Empty;

        [ObservableProperty]
        private string? _filterFabryka;

        public ObservableCollection<ZamowienieDTO> Zamowienia { get; set; } = [];

        public ObservableCollection<string> Fabryki { get; } = [];

        public MainWindowViewModel(DodajZamowienieViewModel childVM, ViewDataProvider dataProvider)
        {
            this.childVM = childVM;
            this.dataProvider = dataProvider;

            foreach (var name in Enum.GetNames<FactoryIdentity>())
                Fabryki.Add(name);

            _ = ZaladujAsync();
        }

        // ── Komendy ─────────────────────────────────────────────────────────────

        [RelayCommand]
        private Task Szukaj() => ZaladujAsync();

        [RelayCommand]
        private void Wyczysc()
        {
            FilterNazwisko = string.Empty;
            FilterVin = string.Empty;
            FilterFabryka = null;
            _ = ZaladujAsync();
        }

        [RelayCommand]
        private void Dodaj() 
        {
            var test = new DodajZamowienieWindow(this.childVM);

            test.Owner = Application.Current.MainWindow;
            if (test.ShowDialog() == true)
                _ = ZaladujAsync();
        }

        [RelayCommand]
        private void Edytuj()
        {

        }

        [RelayCommand]
        private void Usun()
        {

        }

        // ── Prywatne metody ─────────────────────────────────────────────────────

        private async Task ZaladujAsync()
        {
            try
            {
                var test = await this.dataProvider.GetZamowienia();
                foreach(var e in test)
                {
                    Zamowienia.Add(e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas ładowania danych:\n{ex.Message}",
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
