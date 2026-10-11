using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace projekt_bazy_danych
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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