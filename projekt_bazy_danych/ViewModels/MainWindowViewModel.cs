using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using projekt_bazy_danych.Common;
using projekt_bazy_danych.Common.Enums;
using System.Collections.ObjectModel;
using System.Windows;

namespace projekt_bazy_danych.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        //private readonly Datacontext _context;
        private readonly IServiceProvider _services;

        // ── Filtry ──────────────────────────────────────────────────────────────

        [ObservableProperty]
        private string _filterNazwisko = string.Empty;

        [ObservableProperty]
        private string _filterVin = string.Empty;

        [ObservableProperty]
        private string? _filterFabryka;

        // ── Dane ────────────────────────────────────────────────────────────────

        /// <summary>Pozycje wyświetlane w DataGrid.</summary>
        public ObservableCollection<ZamowienieWidokItem> Zamowienia { get; } = [];

        /// <summary>Zaznaczony rekord w DataGrid.</summary>
        [ObservableProperty]
        private ZamowienieWidokItem? _wybraneZamowienie;

        /// <summary>Lista fabryk (enum) do ComboBoxa filtrów.</summary>
        public ObservableCollection<string> Fabryki { get; } = [];

        // ── Konstruktor ─────────────────────────────────────────────────────────

        public MainWindowViewModel(ServiceProvider services)
        {
           // _context = context;

            _services = services;

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
            var okno = _services.GetRequiredService<DodajZamowienieWindow>();
            okno.Owner = Application.Current.MainWindow;
            if (okno.ShowDialog() == true)
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
                //var lista = await Task.Run(() =>
                //{
                //    var query = _context.Zamowienia
                //        .Join(_context.Klienci,
                //              z => z.KlientId,
                //              k => k.Id,
                //              (z, k) => new { Zamowienie = z, Klient = k })
                //        .Join(_context.Pojazdy,
                //              zk => zk.Zamowienie.Vin_pojazdu,
                //              p => p.Vin_pojazdu,
                //              (zk, p) => new
                //              {
                //                  zk.Zamowienie,
                //                  zk.Klient,
                //                  Pojazd = p
                //              })
                //        .AsEnumerable()
                //        .Where(r =>
                //            (string.IsNullOrWhiteSpace(FilterNazwisko) ||
                //             r.Klient.Nazwisko.Contains(FilterNazwisko, StringComparison.OrdinalIgnoreCase)) &&
                //            (string.IsNullOrWhiteSpace(FilterVin) ||
                //             r.Pojazd.Vin_pojazdu.ToString()
                //              .Contains(FilterVin, StringComparison.OrdinalIgnoreCase)) &&
                //            (string.IsNullOrWhiteSpace(FilterFabryka) ||
                //             r.Pojazd.Fabryka.ToString()
                //              .Equals(FilterFabryka, StringComparison.OrdinalIgnoreCase)))
                //        .Select(r => new ZamowienieWidokItem
                //        {
                //            ImieKlienta       = r.Klient.Imie,
                //            NazwiskoKlienta   = r.Klient.Nazwisko,
                //            VinPojazdu        = r.Pojazd.Vin_pojazdu.ToString(),
                //            ModelPojazdu      = r.Pojazd.Model,
                //            KosztZamowienia   = r.Zamowienie.Koszt,
                //            MiejscowoscFabryki = r.Pojazd.Fabryka.ToString()
                //        })
                //        .ToList();

                //    return query;
                //});

                //Zamowienia.Clear();
                //foreach (var item in lista)
                //    Zamowienia.Add(item);
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
