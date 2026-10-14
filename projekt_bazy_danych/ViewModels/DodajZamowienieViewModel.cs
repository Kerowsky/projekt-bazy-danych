using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;

namespace projekt_bazy_danych.ViewModels
{
    public partial class DodajZamowienieViewModel : ObservableObject
    {
     
        public Action<bool>? ZamknijOkno { get; set; }


        [ObservableProperty]
        private string _koszt = string.Empty;

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

            string tekst = Koszt.Trim().Replace('.', ',');
            if (!decimal.TryParse(tekst, NumberStyles.Number,
                    CultureInfo.GetCultureInfo("pl-PL"), out decimal koszt) || koszt < 0)
            {
                MessageBox.Show("Koszt musi być liczbą nieujemną.", "Błąd walidacji",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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
}
