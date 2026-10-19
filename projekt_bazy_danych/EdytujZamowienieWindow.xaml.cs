using projekt_bazy_danych.ViewModels;
using System.Windows;

namespace projekt_bazy_danych
{
    public partial class EdytujZamowienieWindow : Window
    {
        public EdytujZamowienieWindow(EdytujZamowienieViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            viewModel.ZamknijOkno = result => DialogResult = result;
        }
    }
}
