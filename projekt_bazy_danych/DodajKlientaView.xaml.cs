using projekt_bazy_danych.ViewModels;
using System.Windows;


namespace projekt_bazy_danych
{
    /// <summary>
    /// Interaction logic for DodajKlientaView.xaml
    /// </summary>
    public partial class DodajKlientaView : Window
    {
        public DodajKlientaView(DodajKlientaViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}

