using projekt_bazy_danych.ViewModels;
using System.Windows;

namespace projekt_bazy_danych
{
    public partial class DodajZamowienieWindow : Window
    {
        public DodajZamowienieWindow(DodajZamowienieViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            // VM nie ma referencji do Window — zamknięcie przez akcję zwrotną
            viewModel.ZamknijOkno = result => DialogResult = result;
        }
    }
}
