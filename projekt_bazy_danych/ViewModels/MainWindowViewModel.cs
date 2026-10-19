using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using projekt_bazy_danych.Common;
using projekt_bazy_danych.Common.Constants;
using projekt_bazy_danych.Common.Enums;
using projekt_bazy_danych.Common.Providers;
using projekt_bazy_danych.Common.Services;
using projekt_bazy_danych.Common.Utils;
using projekt_bazy_danych.Domain.DTOs;
using System.Collections.ObjectModel;
using System.Windows;

namespace projekt_bazy_danych.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        #region Fields
        private readonly DataProvider dataProvider;

        private readonly DodajZamowienieViewModel dodajZamowienieVM;

        private readonly DodajKlientaViewModel dodajKlientaVM;
        private readonly DodajPojazdViewModel dodajPojazdVM;
        private readonly EdytujZamowienieViewModel edytujZamowienieVM;
        private readonly CsvExportService csvExportService;

        private bool czyCzyszczeFiltry;
        #endregion

        #region Properties
        public ObservableCollection<SzczegolyZamowieniaDTO> Pojazdy { get; set; } = [];

        [ObservableProperty]
        private SzczegolyZamowieniaDTO? wybraneZamowienie;

        [ObservableProperty]
        private string? filterImie;

        [ObservableProperty]
        private string? filterNazwisko;

        [ObservableProperty]
        private string? filterVin;

        public ObservableCollection<string> Marki { get; } = [];

        [ObservableProperty]
        private string? selectedMarka = FilterConstants.brakWyboru;

        public ObservableCollection<string> Modele { get; } = [];

        [ObservableProperty]
        private string? selectedModel = FilterConstants.brakWyboru;

        public ObservableCollection<string> TypySilnika { get; } = [];

        [ObservableProperty]
        private string? selectedTypSilnika = FilterConstants.brakWyboru;

        public ObservableCollection<string> Fabryki { get; } = [];

        [ObservableProperty]
        private string? selectedFactory = FilterConstants.brakWyboru;

        [ObservableProperty]
        private string? filterKosztOd;

        [ObservableProperty]
        private string? filterKosztDo;

        [ObservableProperty]
        private string? filterMocOd;

        [ObservableProperty]
        private string? filterMocDo;

        [ObservableProperty]
        private string? filterPojemnoscOd;

        [ObservableProperty]
        private string? filterPojemnoscDo;

        [ObservableProperty]
        private DateTime? filterDataOd;

        [ObservableProperty]
        private DateTime? filterDataDo;

        /// <summary>
        /// Alias komendy usuwania dla zachowania zgodności z wiązaniami w XAML.
        /// </summary>
        public IRelayCommand UsunCommand => usunCommand;

        /// <summary>
        /// Alias komendy edycji dla zachowania zgodności z wiązaniami w XAML.
        /// </summary>
        public IRelayCommand EdytujCommand => edytujCommand;

        /// <summary>
        /// Alias komendy eksportu CSV dla zachowania zgodności z wiązaniami w XAML.
        /// </summary>
        public IRelayCommand EksportujCsvCommand => eksportujCsvCommand;
        #endregion

        #region Constructors
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="MainWindowViewModel"/> wraz ze słownikami filtrów.
        /// </summary>
        /// <param name="dodajZamowienieVM">Model widoku dodawania zamówienia.</param>
        /// <param name="dodajKlientaVM">Model widoku dodawania klienta.</param>
        /// <param name="dodajPojazdVM">Model widoku dodawania pojazdu.</param>
        /// <param name="dataProvider">Dostawca danych widoku.</param>
        /// <param name="edytujZamowienieVM">Model widoku edycji zamówienia.</param>
        /// <param name="csvExportService">Serwis eksportu danych do formatu CSV.</param>
        public MainWindowViewModel(
            DodajZamowienieViewModel dodajZamowienieVM,
            DodajKlientaViewModel dodajKlientaVM,
            DodajPojazdViewModel dodajPojazdVM,
            DataProvider dataProvider,
            EdytujZamowienieViewModel edytujZamowienieVM,
            CsvExportService csvExportService)
        {
            this.dodajZamowienieVM = dodajZamowienieVM;
            this.dodajKlientaVM = dodajKlientaVM;
            this.dodajPojazdVM = dodajPojazdVM;
            this.dataProvider = dataProvider;
            this.edytujZamowienieVM = edytujZamowienieVM;
            this.csvExportService = csvExportService;

            inicjalizujFiltry();
        }
        #endregion

        #region Commands
        [RelayCommand]
        private Task Szukaj() => ZaladujDaneDoTabeliAsync();

        [RelayCommand]
        private void Dodaj() 
        {
            var test = new DodajZamowienieWindow(this.dodajZamowienieVM);

            test.Owner = Application.Current.MainWindow;
            bool? wynik = test.ShowDialog();
            if (wynik == true)
            {
                _ = ZaladujDaneDoTabeliAsync();
            }
        }

        /// <summary>
        /// Wywołuje asynchroniczną komendę otwarcia okna edycji dla zaznaczonego zamówienia.
        /// </summary>
        [RelayCommand]
        private void edytuj()
        {
            if (WybraneZamowienie == null)
            {
                MessageBox.Show(
                    "Proszę zaznaczyć w tabeli zamówienie, które ma zostać poddane edycji.",
                    "Brak wyboru",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            this.edytujZamowienieVM.zaladujDane(WybraneZamowienie);

            var window = new EdytujZamowienieWindow(this.edytujZamowienieVM);
            window.Owner = Application.Current.MainWindow;
            bool? wynik = window.ShowDialog();

            if (wynik == true)
            {
                _ = ZaladujDaneDoTabeliAsync();
            }
        }

        [RelayCommand]
        private void DodajKlienta()
        {
            var test = new DodajKlientaView(this.dodajKlientaVM);

            test.Owner = Application.Current.MainWindow;
            test.ShowDialog();
        }

        [RelayCommand]
        private void PokazWykres()
        {
            var window = new ChartWindow();
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }

        [RelayCommand]
        private void WyczyscTabele()
        {
            var window = new DodajPojazdView(this.dodajPojazdVM);
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();

            Pojazdy.Clear();
        }

        /// <summary>
        /// Wywołuje asynchroniczną komendę usunięcia zaznaczonego zamówienia.
        /// </summary>
        [RelayCommand]
        private void usun()
        {
            _ = usunAsync();
        }

        /// <summary>
        /// Wywołuje asynchroniczną komendę eksportu przefiltrowanych zamówień do formatu CSV.
        /// </summary>
        [RelayCommand]
        private void eksportujCsv()
        {
            _ = eksportujCsvAsync();
        }

        /// <summary>
        /// Czyści wszystkie ustawione filtry i ładuje domyślne dane.
        /// </summary>
        [RelayCommand]
        private void wyczyscFiltry()
        {
            czyCzyszczeFiltry = true;
            try
            {
                FilterImie = null;
                FilterNazwisko = null;
                FilterVin = null;
                SelectedMarka = FilterConstants.brakWyboru;
                SelectedModel = FilterConstants.brakWyboru;
                SelectedTypSilnika = FilterConstants.brakWyboru;
                SelectedFactory = FilterConstants.brakWyboru;
                FilterKosztOd = null;
                FilterKosztDo = null;
                FilterMocOd = null;
                FilterMocDo = null;
                FilterPojemnoscOd = null;
                FilterPojemnoscDo = null;
                FilterDataOd = null;
                FilterDataDo = null;
            }
            finally
            {
                czyCzyszczeFiltry = false;
            }

            _ = ZaladujDaneDoTabeliAsync();
        }
        #endregion

        #region Events
        /// <summary>
        /// Event wywoływany w momencie zmiany comboboxa FABRYKA
        /// </summary>
        /// <param name="value">Nowa wybrana fabryka.</param>
        partial void OnSelectedFactoryChanged(string? value)
        {
            if (!czyCzyszczeFiltry)
            {
                _ = ZaladujDaneDoTabeliAsync();
            }
        }

        /// <summary>
        /// Event wywoływany w momencie zmiany comboboxa MARKA
        /// </summary>
        /// <param name="value">Nowa wybrana marka.</param>
        partial void OnSelectedMarkaChanged(string? value)
        {
            if (!czyCzyszczeFiltry)
            {
                _ = ZaladujDaneDoTabeliAsync();
            }
        }

        /// <summary>
        /// Event wywoływany w momencie zmiany comboboxa MODEL
        /// </summary>
        /// <param name="value">Nowy wybrany model.</param>
        partial void OnSelectedModelChanged(string? value)
        {
            if (!czyCzyszczeFiltry)
            {
                _ = ZaladujDaneDoTabeliAsync();
            }
        }

        /// <summary>
        /// Event wywoływany w momencie zmiany comboboxa TYP SILNIKA
        /// </summary>
        /// <param name="value">Nowy wybrany typ silnika.</param>
        partial void OnSelectedTypSilnikaChanged(string? value)
        {
            if (!czyCzyszczeFiltry)
            {
                _ = ZaladujDaneDoTabeliAsync();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Wypełnia kolekcje opcji filtrowania (fabryki, typy silnika, marki, modele).
        /// </summary>
        private void inicjalizujFiltry()
        {
            Fabryki.Add(FilterConstants.brakWyboru);
            foreach (var name in Enum.GetNames<FactoryIdentity>())
            {
                Fabryki.Add(name);
            }

            TypySilnika.Add(FilterConstants.brakWyboru);
            foreach (var name in Enum.GetNames<EngineType>())
            {
                TypySilnika.Add(name);
            }

            Marki.Add(FilterConstants.brakWyboru);
            Marki.Add("Alfa Romeo");
            Marki.Add("Fiat");
            Marki.Add("Jeep");
            Marki.Add("Lancia");

            Modele.Add(FilterConstants.brakWyboru);
            Modele.Add("500");
            Modele.Add("Avenger");
            Modele.Add("Panda");
            Modele.Add("Tonale");
            Modele.Add("Ypsilon");

            SelectedFactory = FilterConstants.brakWyboru;
            SelectedTypSilnika = FilterConstants.brakWyboru;
            SelectedMarka = FilterConstants.brakWyboru;
            SelectedModel = FilterConstants.brakWyboru;
        }

        /// <summary>
        /// Buduje obiekt parametrów filtrowania na podstawie wartości wprowadzonych w UI.
        /// </summary>
        /// <returns>Zbudowany obiekt ZamowienieFiltrDTO.</returns>
        private ZamowienieFiltrDTO przygotujFiltr()
        {
            return new ZamowienieFiltrDTO
            {
                Imie = string.IsNullOrWhiteSpace(FilterImie) ? null : FilterImie.Trim(),
                Nazwisko = string.IsNullOrWhiteSpace(FilterNazwisko) ? null : FilterNazwisko.Trim(),
                Vin = int.TryParse(FilterVin?.Trim(), out int vinVal) ? vinVal : null,
                Marka = string.IsNullOrWhiteSpace(SelectedMarka) || SelectedMarka == FilterConstants.brakWyboru ? null : SelectedMarka,
                Model = string.IsNullOrWhiteSpace(SelectedModel) || SelectedModel == FilterConstants.brakWyboru ? null : SelectedModel,
                TypSilnika = SelectedTypSilnika == FilterConstants.brakWyboru ? null : (Enum.TryParse<EngineType>(SelectedTypSilnika, out EngineType silnik) ? silnik : null),
                Fabryka = SelectedFactory == FilterConstants.brakWyboru ? null : (Enum.TryParse<FactoryIdentity>(SelectedFactory, out FactoryIdentity fabryka) ? fabryka : null),
                KosztOd = int.TryParse(FilterKosztOd?.Trim(), out int kOd) ? kOd : null,
                KosztDo = int.TryParse(FilterKosztDo?.Trim(), out int kDo) ? kDo : null,
                MocOd = int.TryParse(FilterMocOd?.Trim(), out int mOd) ? mOd : null,
                MocDo = int.TryParse(FilterMocDo?.Trim(), out int mDo) ? mDo : null,
                PojemnoscOd = int.TryParse(FilterPojemnoscOd?.Trim(), out int pOd) ? pOd : null,
                PojemnoscDo = int.TryParse(FilterPojemnoscDo?.Trim(), out int pDo) ? pDo : null,
                DataOd = FilterDataOd,
                DataDo = FilterDataDo
            };
        }

        private async Task ZaladujDaneDoTabeliAsync()
        {
            Pojazdy.Clear();
            try
            {
                ZamowienieFiltrDTO filtr = przygotujFiltr();
                ObservableCollection<SzczegolyZamowieniaDTO> zamowieniaDTO = await this.dataProvider.GetZamowieniaSzczegoly(filtr);
                foreach (var e in zamowieniaDTO)
                {
                    Pojazdy.Add(e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas ładowania danych:\n{ex.Message}",
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Asynchronicznie usuwa zaznaczone zamówienie z bazy danych po uprzednim potwierdzeniu przez użytkownika.
        /// </summary>
        private async Task usunAsync()
        {
            if (WybraneZamowienie == null)
            {
                MessageBox.Show(
                    "Proszę zaznaczyć w tabeli zamówienie, które ma zostać usunięte.",
                    "Brak wyboru",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult potwierdzenie = MessageBox.Show(
                $"Czy na pewno chcesz usunąć zamówienie dla klienta {WybraneZamowienie.Imie} {WybraneZamowienie.Nazwisko} (VIN: {WybraneZamowienie.Vin_pojazdu})?",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (potwierdzenie != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                ResultWithMessage result = await dataProvider.UsunZamowienie(WybraneZamowienie.Id_zamowienia);

                if (!result.IsValid)
                {
                    MessageBox.Show(result.Message, "Błąd usuwania", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show(result.Message, "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    WybraneZamowienie = null;
                    await ZaladujDaneDoTabeliAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas usuwania zamówienia:\n{ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Otwiera okno dialogowe zapisu pliku i asynchronicznie eksportuje aktualnie wyfiltrowane zamówienia do formatu CSV.
        /// </summary>
        private async Task eksportujCsvAsync()
        {
            if (Pojazdy == null || Pojazdy.Count == 0)
            {
                MessageBox.Show(
                    "Brak danych do wyeksportowania. Zmień kryteria filtrowania, aby wyświetlić zamówienia.",
                    "Brak danych",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Title = "Eksportuj zamówienia do pliku CSV",
                Filter = "Pliki CSV (*.csv)|*.csv|Wszystkie pliki (*.*)|*.*",
                FileName = $"zamowienia_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                DefaultExt = ".csv"
            };

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            try
            {
                ResultWithMessage result = await csvExportService.EksportujZamowieniaDoCsv(dialog.FileName, Pojazdy);

                if (!result.IsValid)
                {
                    MessageBox.Show(result.Message, "Błąd eksportu", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show(result.Message, "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas eksportowania do CSV:\n{ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
