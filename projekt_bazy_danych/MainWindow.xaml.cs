using projekt_bazy_danych.ViewModels;
using System.Windows;

namespace projekt_bazy_danych
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}