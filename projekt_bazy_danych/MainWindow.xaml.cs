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
    }
}