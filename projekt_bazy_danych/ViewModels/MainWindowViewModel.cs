using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using projekt_bazy_danych.Common;
using projekt_bazy_danych.Common.Constants;
using projekt_bazy_danych.Common.Enums;
using projekt_bazy_danych.Common.Providers;
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
        private string? selectedMarka;

        public ObservableCollection<string> Modele { get; } = [];

        [ObservableProperty]
        private string? selectedModel;

        public ObservableCollection<string> TypySilnika { get; } = [];

        [ObservableProperty]
        private string? selectedTypSilnika;

        public ObservableCollection<string> Fabryki { get; } = [];

        [ObservableProperty]
        private string? selectedFactory;

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
        #endregion

        #region Constructors
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="MainWindowViewModel"/> wraz ze słownikami filtrów.
        /// </summary>
        /// <param name="dodajZamowienieVM">Model widoku dodawania zamówienia.</param>
        /// <param name="dodajKlientaVM">Model widoku dodawania klienta.</param>
        /// <param name="dodajPojazdVM">Model widoku dodawania pojazdu.</param>
        /// <param name="dataProvider">Dostawca danych widoku.</param>
        public MainWindowViewModel(
            DodajZamowienieViewModel dodajZamowienieVM,
            DodajKlientaViewModel dodajKlientaVM,
            DodajPojazdViewModel dodajPojazdVM,
            DataProvider dataProvider)
        {
            this.dodajZamowienieVM = dodajZamowienieVM;
            this.dodajKlientaVM = dodajKlientaVM;
            this.dodajPojazdVM = dodajPojazdVM;
            this.dataProvider = dataProvider;

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
            test.ShowDialog();
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
                SelectedMarka = null;
                SelectedModel = null;
                SelectedTypSilnika = null;
                SelectedFactory = null;
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
        #endregion
    }
}
