using projekt_bazy_danych.Common;
using projekt_bazy_danych.Common.Repositories;
using projekt_bazy_danych.Entities;
using System.Windows;

namespace projekt_bazy_danych
{
    public partial class MainWindow : Window
    {
        private KlientRepository klientRepository;

        private Repository<Pojazd> pojazdRepository;

        private Repository<Zamowienie> zamowienieReposiotry;


        public MainWindow()
        {
            InitializeComponent();

            Datacontext dataContext = new Datacontext();
            klientRepository = new KlientRepository(dataContext);
            pojazdRepository = new Repository<Pojazd>(dataContext);
            zamowienieReposiotry = new Repository<Zamowienie>(dataContext);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gridPojazdy.ItemsSource = await pojazdRepository.GetValuesAsync();
            gridKlienci.ItemsSource = await klientRepository.GetAllKlient();
            gridZamowienia.ItemsSource = await zamowienieReposiotry.GetValuesAsync();
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

    // Klasa modelu widoku (DTO) niezbędna do prawidłowego bindowania danych w DataGrid
    public class ZamowienieWidok
    {
        public string ImieKlienta { get; set; }
        public string NazwiskoKlienta { get; set; }
        public string VinPojazdu { get; set; }
        public string ModelPojazdu { get; set; }
        public decimal KosztZamowienia { get; set; }
        public string MiejscowoscFabryki { get; set; }
    }
}