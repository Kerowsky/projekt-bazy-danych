using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using projekt_bazy_danych.Common;
using projekt_bazy_danych.Common.Enums;
using projekt_bazy_danych.Common.Services;
using projekt_bazy_danych.Common.Utils;
using projekt_bazy_danych.Domain.DTOs;
using System.Collections.ObjectModel;
using System.Windows;

namespace projekt_bazy_danych.ViewModels
{
    /// <summary>
    /// Model widoku formularza edycji istniejącego zamówienia.
    /// </summary>
    public partial class EdytujZamowienieViewModel : ObservableObject
    {
        #region Fields
        private readonly Datacontext context;

        private readonly EditDataService editDataService;

        private Guid idZamowienia;

        private bool czyLadowanieDanych;
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
        private DateTime? dataZamowienia;

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
        /// Inicjalizuje nową instancję klasy <see cref="EdytujZamowienieViewModel"/>.
        /// </summary>
        /// <param name="context">Kontekst bazy danych Entity Framework.</param>
        /// <param name="editDataService">Serwis edycji danych.</param>
        public EdytujZamowienieViewModel(
            Datacontext context,
            EditDataService editDataService)
        {
            this.context = context;
            this.editDataService = editDataService;

            inicjalizujListy();
        }
        #endregion

        #region Commands
        /// <summary>
        /// Wywołuje asynchroniczną komendę zapisu zmian w edytowanym zamówieniu.
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
            if (!czyLadowanieDanych)
            {
                aktualizujModele(value);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Ładuje dane zamówienia z obiektu DTO do pól formularza edycji.
        /// </summary>
        /// <param name="zamowienie">Szczegóły wybranego zamówienia z tabeli.</param>
        public void zaladujDane(SzczegolyZamowieniaDTO zamowienie)
        {
            czyLadowanieDanych = true;
            try
            {
                idZamowienia = zamowienie.Id_zamowienia;
                Imie = zamowienie.Imie;
                Nazwisko = zamowienie.Nazwisko;
                Pesel = zamowienie.Pesel;
                Vin = zamowienie.Vin_pojazdu.ToString();
                SelectedMarka = zamowienie.Marka;
                aktualizujModele(zamowienie.Marka);
                SelectedModel = zamowienie.Model;
                SelectedTypSilnika = zamowienie.Typ_silnika.ToString();
                SelectedFactory = zamowienie.Fabryka.ToString();
                Moc = zamowienie.Moc.ToString();
                Pojemnosc = zamowienie.Pojemnosc.ToString();
                Koszt = zamowienie.Koszt.ToString();
                DataZamowienia = zamowienie.Data;
            }
            finally
            {
                czyLadowanieDanych = false;
            }
        }

        /// <summary>
        /// Inicjalizuje kolekcje marek, typów silników i fabryk dla kontrolek formularza.
        /// </summary>
        private void inicjalizujListy()
        {
            Marki.Clear();
            Marki.Add("Fiat");
            Marki.Add("Ford");
            Marki.Add("Lancia");
            Marki.Add("Syrena");
            Marki.Add("Volkswagen");

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
                case "Fiat":
                    Modele.Add("126p");
                    Modele.Add("126p BIS");
                    Modele.Add("127p");
                    Modele.Add("500");
                    Modele.Add("Cinquecento");
                    Modele.Add("Palio Weekend");
                    Modele.Add("Seicento");
                    Modele.Add("Siena");
                    Modele.Add("Uno");
                    break;
                case "Ford":
                    Modele.Add("Ka");
                    Modele.Add("Tourneo Connect");
                    Modele.Add("Transit Connect");
                    break;
                case "Lancia":
                    Modele.Add("Ypsilon");
                    break;
                case "Syrena":
                    Modele.Add("105");
                    Modele.Add("105L");
                    Modele.Add("Bosto");
                    Modele.Add("R-20");
                    break;
                case "Volkswagen":
                    Modele.Add("Caddy");
                    Modele.Add("Caddy California");
                    Modele.Add("Caddy Cargo");
                    break;
                default:
                    Modele.Add("105");
                    Modele.Add("105L");
                    Modele.Add("126p");
                    Modele.Add("126p BIS");
                    Modele.Add("127p");
                    Modele.Add("500");
                    Modele.Add("Bosto");
                    Modele.Add("Caddy");
                    Modele.Add("Caddy California");
                    Modele.Add("Caddy Cargo");
                    Modele.Add("Cinquecento");
                    Modele.Add("Ka");
                    Modele.Add("Palio Weekend");
                    Modele.Add("R-20");
                    Modele.Add("Seicento");
                    Modele.Add("Siena");
                    Modele.Add("Tourneo Connect");
                    Modele.Add("Transit Connect");
                    Modele.Add("Uno");
                    Modele.Add("Ypsilon");
                    break;
            }

            SelectedModel = Modele.FirstOrDefault();
        }

        /// <summary>
        /// Asynchronicznie zapisuje edytowane dane w bazie danych i informuje użytkownika o wyniku.
        /// </summary>
        private async Task zapiszAsync()
        {
            ResultWithMessage result = await editDataService.EditZamowienie(
                idZamowienia,
                Imie,
                Nazwisko,
                Pesel,
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
                ZamknijOkno?.Invoke(true);
            }
        }
        #endregion
    }
}
