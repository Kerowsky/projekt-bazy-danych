using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using projekt_bazy_danych.Common;
using projekt_bazy_danych.Common.Enums;
using projekt_bazy_danych.Common.Services;
using projekt_bazy_danych.Entities;
using System.Collections.ObjectModel;
using System.Windows;

namespace projekt_bazy_danych.ViewModels
{
    /// <summary>
    /// Model widoku odpowiedzialny za formularz dodawania nowego pojazdu.
    /// </summary>
    public partial class DodajPojazdViewModel : ObservableObject
    {
        #region Fields
        private readonly Datacontext context;

        private readonly AddDataService addDataService;
        #endregion

        #region Properties
        [ObservableProperty]
        private string? vin;

        [ObservableProperty]
        private string? model;

        [ObservableProperty]
        private EngineType? wybranyTypSilnika;

        [ObservableProperty]
        private string? moc;

        [ObservableProperty]
        private string? pojemnosc;

        [ObservableProperty]
        private FactoryIdentity? wybranaFabryka;

        public ObservableCollection<EngineType> TypySilnika { get; } = [];

        public ObservableCollection<FactoryIdentity> Fabryki { get; } = [];
        #endregion

        #region Constructors
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="DodajPojazdViewModel"/>.
        /// </summary>
        /// <param name="context">Kontekst bazy danych Entity Framework.</param>
        /// <param name="addDataService">Serwis dodawania danych.</param>
        public DodajPojazdViewModel(
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
        /// Wywołuje asynchroniczną metodę dodawania pojazdu.
        /// </summary>
        [RelayCommand]
        private void dodajPojazd()
        {
            _ = dodajPojazdAsync();
        }
        #endregion

        #region Events
        #endregion

        #region Methods
        /// <summary>
        /// Wypełnia kolekcje enumów dla typów silnika oraz fabryk.
        /// </summary>
        private void inicjalizujListy()
        {
            foreach (EngineType typ in Enum.GetValues<EngineType>())
            {
                TypySilnika.Add(typ);
            }

            foreach (FactoryIdentity fabryka in Enum.GetValues<FactoryIdentity>())
            {
                Fabryki.Add(fabryka);
            }

            WybranyTypSilnika = TypySilnika.FirstOrDefault();
            WybranaFabryka = Fabryki.FirstOrDefault();
        }

        /// <summary>
        /// Waliduje dane wejściowe i asynchronicznie zapisuje nowy pojazd w bazie danych.
        /// </summary>
        private async Task dodajPojazdAsync()
        {
            if (string.IsNullOrWhiteSpace(Vin) || !int.TryParse(Vin.Trim(), out int vinNumer) || vinNumer <= 0)
            {
                MessageBox.Show("VIN musi być poprawną liczbą całkowitą dodatnią.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(Model))
            {
                MessageBox.Show("Model pojazdu nie może być pusty.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (WybranyTypSilnika == null)
            {
                MessageBox.Show("Wybierz typ silnika.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(Moc) || !int.TryParse(Moc.Trim(), out int mocWartosc) || mocWartosc <= 0)
            {
                MessageBox.Show("Moc musi być poprawną liczbą dodatnią.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(Pojemnosc) || !int.TryParse(Pojemnosc.Trim(), out int pojemnoscWartosc) || pojemnoscWartosc < 0)
            {
                MessageBox.Show("Pojemność musi być poprawną liczbą całkowitą nieujemną.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (WybranaFabryka == null)
            {
                MessageBox.Show("Wybierz fabrykę.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                bool czyIstnieje = await context.Pojazdy.AnyAsync(p => p.Vin_pojazdu == vinNumer);
                if (czyIstnieje)
                {
                    MessageBox.Show($"Pojazd o podanym numerze VIN ({vinNumer}) już istnieje w bazie danych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Pojazd nowyPojazd = new Pojazd
                {
                    Vin_pojazdu = vinNumer,
                    Model = Model.Trim(),
                    Typ_silnika = WybranyTypSilnika.Value,
                    Moc = mocWartosc,
                    Pojemnosc = pojemnoscWartosc,
                    Fabryka = WybranaFabryka.Value
                };

                await context.Pojazdy.AddAsync(nowyPojazd);
                await context.SaveChangesAsync();

                MessageBox.Show("Pomyślnie dodano pojazd do bazy danych.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                wyczyscFormularz();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas dodawania pojazdu do bazy danych:\n{ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Czyści pola formularza po udanym zapisie.
        /// </summary>
        private void wyczyscFormularz()
        {
            Vin = string.Empty;
            Model = string.Empty;
            Moc = string.Empty;
            Pojemnosc = string.Empty;
        }
        #endregion
    }
}
