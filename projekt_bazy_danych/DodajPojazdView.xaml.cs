using projekt_bazy_danych.ViewModels;
using System.Windows;

namespace projekt_bazy_danych
{
    /// <summary>
    /// Logika interakcji dla okna DodajPojazdView.xaml.
    /// </summary>
    public partial class DodajPojazdView : Window
    {
        /// <summary>
        /// Inicjalizuje nową instancję okna <see cref="DodajPojazdView"/>.
        /// </summary>
        /// <param name="viewModel">Instancja modelu widoku dla dodawania pojazdu.</param>
        public DodajPojazdView(DodajPojazdViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
