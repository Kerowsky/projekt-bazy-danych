using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Collections;
using System.Globalization;


namespace projekt_bazy_danych
{
    public partial class DodajZamowienieWindow : Window
    {
        // Wyniki dostępne dla okna głównego po ShowDialog() == true
        public object? WybranyKlientId { get; private set; }
        public object? WybranyPojazdVin { get; private set; }
        public decimal Koszt { get; private set; }

        public DodajZamowienieWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Wywołaj przed ShowDialog(), przekazując dane z bazy.
        /// Elementy klientów muszą mieć publiczne właściwości PelneImie i IdKlienta,
        /// pojazdów: NazwaModelu i VinPojazdu.
        /// </summary>
        public void ZaladujListyRozwijane(IEnumerable klienci, IEnumerable pojazdy)
        {
            cmbKlient.ItemsSource = klienci;
            cmbPojazd.ItemsSource = pojazdy;
        }

        private void BtnZapisz_Click(object sender, RoutedEventArgs e)
        {
            if (cmbKlient.SelectedValue == null)
            {
                Blad("Wybierz klienta.", cmbKlient);
                return;
            }

            if (cmbPojazd.SelectedValue == null)
            {
                Blad("Wybierz pojazd.", cmbPojazd);
                return;
            }

            // Akceptuj zarówno przecinek, jak i kropkę
            string tekst = txtKoszt.Text.Trim().Replace('.', ',');
            if (!decimal.TryParse(tekst, NumberStyles.Number,
                    CultureInfo.GetCultureInfo("pl-PL"), out decimal koszt) || koszt < 0)
            {
                Blad("Koszt musi być liczbą nieujemną.", txtKoszt);
                return;
            }

            WybranyKlientId = cmbKlient.SelectedValue;
            WybranyPojazdVin = cmbPojazd.SelectedValue;
            Koszt = koszt;

            // INSERT do bazy zrób tutaj albo w oknie głównym po ShowDialog()

            DialogResult = true; // zamyka okno samo
        }

        private void Blad(string komunikat, UIElement kontrolka)
        {
            MessageBox.Show(this, komunikat, "Błąd walidacji",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            kontrolka.Focus();
        }
    }
}