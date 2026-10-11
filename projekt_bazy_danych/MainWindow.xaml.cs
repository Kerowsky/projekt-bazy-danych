using projekt_bazy_danych.Common;
using projekt_bazy_danych.Common.Repositories;
using projekt_bazy_danych.Entities;
using System.Windows;

namespace projekt_bazy_danych
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }
        private void BtnSzukaj_Click(object sender, RoutedEventArgs e)
        {
            // Logika filtrowania bazy danych
        }

        private void BtnWyczysc_Click(object sender, RoutedEventArgs e)
        {
            // Resetowanie pól tekstowych i ComboBoxa
            txtFilterNazwisko.Text = string.Empty;

            txtFilterVin.Text = string.Empty;
            cmbFilterFabryka.SelectedIndex = -1;
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            var okno = new DodajZamowienieWindow { Owner = this };
            if (okno.ShowDialog() == true)
            {
                // kliknięto Zapisz: tu odśwież tabelę
            }
        }

        private void BtnEdytuj_Click(object sender, RoutedEventArgs e)
        {
            // Sprawdzenie czy wybrano rekord i otwarcie okna edycji
        }

        private void BtnUsun_Click(object sender, RoutedEventArgs e)
        {
            // Sprawdzenie czy wybrano rekord, potwierdzenie akcji i usunięcie z bazy
        }
    }
}