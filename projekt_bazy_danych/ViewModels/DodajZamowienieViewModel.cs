using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using projekt_bazy_danych.Common;
using projekt_bazy_danych.Entities;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;

namespace projekt_bazy_danych.ViewModels
{
    /// <summary>
    /// ViewModel dla okna "Dodaj zamówienie".
    /// Akcja <see cref="ZamknijOkno"/> jest wstrzykiwana przez code-behind
    /// i wywołuje Window.DialogResult = true po udanej walidacji.
    /// </summary>
    public partial class DodajZamowienieViewModel : ObservableObject
    {
      

        // Wywoływane przez code-behind po pomyślnym zapisie, ustawia DialogResult = true
        public Action<bool>? ZamknijOkno { get; set; }

        // ── Listy rozwijane ──────────────────────────────────────────────────────

        public ObservableCollection<KlientItem> Klienci { get; } = [];
        public ObservableCollection<PojazdItem> Pojazdy { get; } = [];

        // ── Pola formularza ──────────────────────────────────────────────────────

        [ObservableProperty]
        private KlientItem? _wybranyKlient;

        [ObservableProperty]
        private PojazdItem? _wybranyPojazd;

        [ObservableProperty]
        private string _koszt = string.Empty;

        // ── Wyniki (dostępne po zamknięciu okna) ────────────────────────────────

        public Guid? WybranyKlientId => WybranyKlient?.IdKlienta;
        public int? WybranyPojazdVin => WybranyPojazd?.VinPojazdu;
        public decimal KosztDecimal { get; private set; }

        // ── Konstruktor ─────────────────────────────────────────────────────────

        public DodajZamowienieViewModel()
        {
            // _context = context;
            //_ = ZaladujAsync();
        }

        // ── Komendy ─────────────────────────────────────────────────────────────

        [RelayCommand]
        private void Zapisz()
        {
            if (WybranyKlient is null)
            {
                MessageBox.Show("Wybierz klienta.", "Błąd walidacji",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (WybranyPojazd is null)
            {
                MessageBox.Show("Wybierz pojazd.", "Błąd walidacji",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string tekst = Koszt.Trim().Replace('.', ',');
            if (!decimal.TryParse(tekst, NumberStyles.Number,
                    CultureInfo.GetCultureInfo("pl-PL"), out decimal koszt) || koszt < 0)
            {
                MessageBox.Show("Koszt musi być liczbą nieujemną.", "Błąd walidacji",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            KosztDecimal = koszt;

            // TODO: zapis do bazy danych

            ZamknijOkno?.Invoke(true);
        }

        // ── Prywatne metody ─────────────────────────────────────────────────────

        private async Task ZaladujAsync()
        {
            try
            {
                //var klienci = await Task.Run(() =>
                //    _context.Klienci
                //        .Select(k => new KlientItem
                //        {
                //            IdKlienta = k.Id,
                //            PelneImie = k.Imie + " " + k.Nazwisko
                //        })
                //        .ToList());

                //var pojazdy = await Task.Run(() =>
                //    _context.Pojazdy
                //        .Select(p => new PojazdItem
                //        {
                //            VinPojazdu = p.Vin_pojazdu,
                //            NazwaModelu = p.Model
                //        })
                //        .ToList());

                //Klienci.Clear();
                //foreach (var k in klienci) Klienci.Add(k);

                //Pojazdy.Clear();
                //foreach (var p in pojazdy) Pojazdy.Add(p);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas ładowania danych:\n{ex.Message}",
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    // ── Modele pomocnicze dla ComboBoxów ────────────────────────────────────────

    public sealed class KlientItem
    {
        public Guid IdKlienta { get; set; }
        public string PelneImie { get; set; } = string.Empty;
    }

    public sealed class PojazdItem
    {
        public int VinPojazdu { get; set; }
        public string NazwaModelu { get; set; } = string.Empty;
    }
}
