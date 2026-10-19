using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using projekt_bazy_danych.Common;
using projekt_bazy_danych.Common.Enums;
using projekt_bazy_danych.Common.Services;
using projekt_bazy_danych.Common.Utils;
using System.Collections.ObjectModel;
using System.Windows;

namespace projekt_bazy_danych.ViewModels
{
    /// <summary>
    /// Model widoku formularza dodawania nowego zamówienia do bazy danych.
    /// </summary>
    public partial class DodajZamowienieViewModel : ObservableObject
    {
        #region Fields
        private readonly Datacontext context;

        private readonly AddDataService addDataService;
        #endregion

        #region Properties
        public Action<bool>? ZamknijOkno { get; set; }

        [ObservableProperty]
        private string? imie;

        [ObservableProperty]
        private string? nazwisko;

        [ObservableProperty]
        private string? pesel;

        [ObservableProperty]
        private string? vin;

        [ObservableProperty]
        private string? selectedMarka;

        [ObservableProperty]
        private string? selectedModel;

        [ObservableProperty]
        private string? selectedTypSilnika;

        [ObservableProperty]
        private string? selectedFactory;

        [ObservableProperty]
        private string? moc;

        [ObservableProperty]
        private string? pojemnosc;

        [ObservableProperty]
        private string? koszt;

        [ObservableProperty]
        private DateTime? dataZamowienia = DateTime.Now;

        public ObservableCollection<string> Marki { get; } = [];

        public ObservableCollection<string> Modele { get; } = [];

        public ObservableCollection<string> TypySilnika { get; } = [];

        public ObservableCollection<string> Fabryki { get; } = [];

        /// <summary>
        /// Alias komendy zapisu dla zachowania kompatybilności z wiązaniem w XAML.
        /// </summary>
        public IRelayCommand ZapiszCommand => zapiszCommand;
        #endregion

        #region Constructors
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="DodajZamowienieViewModel"/>.
        /// </summary>
        /// <param name="context">Kontekst bazy danych Entity Framework.</param>
        /// <param name="addDataService">Serwis dodawania danych.</param>
        public DodajZamowienieViewModel(
            Datacontext context,
            AddDataService addDataService)
        {
            this.context = context;
            this.addDataService = addDataService;

            inicjalizujListy();
        }
        #endregion

        #region Commands
        /// <summary>
        /// Wywołuje asynchroniczną komendę zapisu nowego zamówienia.
        /// </summary>
        [RelayCommand]
        private void zapisz()
        {
            _ = zapiszAsync();
        }
        #endregion

        #region Events
        /// <summary>
        /// Reaguje na zmianę wybranej marki pojazdu, aktualizując listę powiązanych modeli.
        /// </summary>
        /// <param name="value">Nowa wybrana marka.</param>
        partial void OnSelectedMarkaChanged(string? value)
        {
            aktualizujModele(value);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Inicjalizuje kolekcje marek, typów silników i fabryk dla kontrolek formularza.
        /// </summary>
        private void inicjalizujListy()
        {
            Marki.Clear();
            Marki.Add("Alfa Romeo");
            Marki.Add("Fiat");
            Marki.Add("Jeep");
            Marki.Add("Lancia");

            TypySilnika.Clear();
            foreach (var name in Enum.GetNames<EngineType>())
            {
                TypySilnika.Add(name);
            }

            Fabryki.Clear();
            foreach (var name in Enum.GetNames<FactoryIdentity>())
            {
                Fabryki.Add(name);
            }

            SelectedMarka = Marki.FirstOrDefault();
            SelectedTypSilnika = TypySilnika.FirstOrDefault();
            SelectedFactory = Fabryki.FirstOrDefault();
        }

        /// <summary>
        /// Aktualizuje listę dostępnych modeli pojazdów na podstawie wybranej marki.
        /// </summary>
        /// <param name="marka">Nazwa wybranej marki.</param>
        private void aktualizujModele(string? marka)
        {
            Modele.Clear();

            switch (marka)
            {
                case "Alfa Romeo":
                    Modele.Add("Tonale");
                    break;
                case "Fiat":
                    Modele.Add("500");
                    Modele.Add("Panda");
                    break;
                case "Jeep":
                    Modele.Add("Avenger");
                    break;
                case "Lancia":
                    Modele.Add("Ypsilon");
                    break;
                default:
                    Modele.Add("500");
                    Modele.Add("Avenger");
                    Modele.Add("Panda");
                    Modele.Add("Tonale");
                    Modele.Add("Ypsilon");
                    break;
            }

            SelectedModel = Modele.FirstOrDefault();
        }

        /// <summary>
        /// Asynchronicznie dodaje zamówienie do bazy danych korzystając z serwisu danych i informuje użytkownika o wyniku.
        /// </summary>
        private async Task zapiszAsync()
        {
            ResultWithMessage result = await addDataService.AddNewZamowienie(
                Imie,
                Nazwisko,
                Pesel,
                Vin,
                SelectedMarka,
                SelectedModel,
                SelectedTypSilnika,
                SelectedFactory,
                Moc,
                Pojemnosc,
                Koszt,
                DataZamowienia);

            if (!result.IsValid)
            {
                MessageBox.Show(result.Message, "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show(result.Message, "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                wyczyscFormularz();
                ZamknijOkno?.Invoke(true);
            }
        }

        /// <summary>
        /// Czyści formularz dodawania zamówienia po poprawnym zapisie.
        /// </summary>
        private void wyczyscFormularz()
        {
            Imie = string.Empty;
            Nazwisko = string.Empty;
            Pesel = string.Empty;
            Vin = string.Empty;
            Moc = string.Empty;
            Pojemnosc = string.Empty;
            Koszt = string.Empty;
            DataZamowienia = DateTime.Now;
            SelectedMarka = Marki.FirstOrDefault();
            SelectedTypSilnika = TypySilnika.FirstOrDefault();
            SelectedFactory = Fabryki.FirstOrDefault();
        }
        #endregion
    }
}
