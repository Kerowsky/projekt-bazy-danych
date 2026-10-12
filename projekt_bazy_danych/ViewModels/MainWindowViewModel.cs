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

        [ObservableProperty]
        private ZamowienieWidokItem? _wybraneZamowienie;

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
            if (WybraneZamowienie is null)
            {
                MessageBox.Show("Zaznacz zamówienie do edycji.", "Informacja",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            // TODO: otworzyć okno edycji z WybraneZamowienie
        }

        [RelayCommand]
        private void Usun()
        {
            if (WybraneZamowienie is null)
            {
                MessageBox.Show("Zaznacz zamówienie do usunięcia.", "Informacja",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var wynik = MessageBox.Show(
                $"Czy na pewno chcesz usunąć zamówienie klienta " +
                $"{WybraneZamowienie.ImieKlienta} {WybraneZamowienie.NazwiskoKlienta}?",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (wynik == MessageBoxResult.Yes)
            {
                // TODO: usunąć z bazy danych
                _ = ZaladujAsync();
            }
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

    // ── Model wiersza DataGrid ───────────────────────────────────────────────────

    public sealed class ZamowienieWidokItem
    {
        public string ImieKlienta      { get; set; } = string.Empty;
        public string NazwiskoKlienta  { get; set; } = string.Empty;
        public string VinPojazdu       { get; set; } = string.Empty;
        public string ModelPojazdu     { get; set; } = string.Empty;
        public decimal KosztZamowienia { get; set; }
        public string MiejscowoscFabryki { get; set; } = string.Empty;
    }
}
