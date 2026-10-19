using Microsoft.EntityFrameworkCore;
using projekt_bazy_danych.Common.Enums;
using projekt_bazy_danych.Common.Repositories;
using projekt_bazy_danych.Common.Utils;
using projekt_bazy_danych.Domain.DTOs;
using projekt_bazy_danych.Entities;
using System.Globalization;

namespace projekt_bazy_danych.Common.Services
{
    public sealed class AddDataService
    {
        #region Fields
        private readonly Datacontext datacontext;

        private readonly KlientRepository klientRepository;

        private readonly ZamowienieRepository zamowienieRepository;

        private readonly PojazdRepository pojazdRepository;
        #endregion

        #region Constructors
        public AddDataService(Datacontext datacontext)
        {
            this.datacontext = datacontext;
            this.klientRepository = new KlientRepository(datacontext);
            this.zamowienieRepository = new ZamowienieRepository(datacontext);
            this.pojazdRepository = new PojazdRepository(datacontext);
        }
        #endregion

        #region Methods
        public async Task<ResultWithMessage> AddNewKlient(string imie, string nazwisko, string pesel)
        {
            if(String.IsNullOrEmpty(imie))
            {
                return new ResultWithMessage(false, "Imie nie moze byc puste");
            }

            if (String.IsNullOrEmpty(nazwisko))
            {
                return new ResultWithMessage(false, "Nazwisko nie moze byc puste");
            }

            if (String.IsNullOrEmpty(pesel))
            {
                return new ResultWithMessage(false, "Pesel nie moze byc pusty");
            }

            KlientDTO klientDTO = new KlientDTO()
            {
                Id = new Guid(),
                Imie = imie,
                Nazwisko = nazwisko,
                Pesel = pesel
            };
            ResultWithMessage result = await klientRepository.AddKlient(klientDTO);

            if (!result.IsValid)
            {
                return result;
            }

            return new ResultWithMessage(true, "Pomyslnie dodano klienta do bazy danych");
        }

        public async Task<ResultWithMessage> AddNewPojazd(string imie, string nazwisko, string pesel)
        {
            if (String.IsNullOrEmpty(imie))
            {
                return new ResultWithMessage(false, "Imie nie moze byc puste");
            }

            if (String.IsNullOrEmpty(nazwisko))
            {
                return new ResultWithMessage(false, "Nazwisko nie moze byc puste");
            }

            if (String.IsNullOrEmpty(pesel))
            {
                return new ResultWithMessage(false, "Pesel nie moze byc pusty");
            }

            KlientDTO klient = new KlientDTO(imie, nazwisko, pesel);
            ResultWithMessage result = await klientRepository.AddKlient(klient);

            if (!result.IsValid)
            {
                return result;
            }

            return new ResultWithMessage(true);
        }

        /// <summary>
        /// Dodaje nowe zamówienie do bazy danych wraz z powiązanym klientem i pojazdem.
        /// </summary>
        /// <param name="imie">Imię klienta.</param>
        /// <param name="nazwisko">Nazwisko klienta.</param>
        /// <param name="pesel">Numer PESEL klienta.</param>
        /// <param name="vin">Numer VIN pojazdu (liczba całkowita dodatnia).</param>
        /// <param name="marka">Marka pojazdu.</param>
        /// <param name="model">Model pojazdu.</param>
        /// <param name="typSilnika">Nazwa wybranego typu silnika.</param>
        /// <param name="fabryka">Nazwa wybranej fabryki.</param>
        /// <param name="moc">Moc pojazdu w KM.</param>
        /// <param name="pojemnosc">Pojemność silnika w cm³ (opcjonalna dla silników elektrycznych).</param>
        /// <param name="koszt">Koszt zamówienia w PLN.</param>
        /// <param name="dataZamowienia">Data zamówienia.</param>
        /// <returns>Wynik operacji wraz z komunikatem informacyjnym.</returns>
        public async Task<ResultWithMessage> AddNewZamowienie(
            string? imie,
            string? nazwisko,
            string? pesel,
            string? vin,
            string? marka,
            string? model,
            string? typSilnika,
            string? fabryka,
            string? moc,
            string? pojemnosc,
            string? koszt,
            DateTime? dataZamowienia)
        {
            if (string.IsNullOrWhiteSpace(imie))
            {
                return new ResultWithMessage(false, "Imię klienta nie może być puste.");
            }

            if (string.IsNullOrWhiteSpace(nazwisko))
            {
                return new ResultWithMessage(false, "Nazwisko klienta nie może być puste.");
            }

            if (string.IsNullOrWhiteSpace(pesel))
            {
                return new ResultWithMessage(false, "PESEL klienta nie może być pusty.");
            }

            string peselTrimmed = pesel.Trim();
            if (peselTrimmed.Length != 11 || !peselTrimmed.All(char.IsDigit))
            {
                return new ResultWithMessage(false, "PESEL musi składać się z dokładnie 11 cyfr.");
            }

            if (string.IsNullOrWhiteSpace(vin) || !int.TryParse(vin.Trim(), out int vinNumer) || vinNumer <= 0)
            {
                return new ResultWithMessage(false, "VIN musi być poprawną liczbą całkowitą dodatnią.");
            }

            if (string.IsNullOrWhiteSpace(marka))
            {
                return new ResultWithMessage(false, "Wybierz markę pojazdu.");
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                return new ResultWithMessage(false, "Wybierz model pojazdu.");
            }

            if (string.IsNullOrWhiteSpace(typSilnika) || !Enum.TryParse<EngineType>(typSilnika, out EngineType silnikEnum))
            {
                return new ResultWithMessage(false, "Wybierz poprawny typ silnika.");
            }

            if (string.IsNullOrWhiteSpace(fabryka) || !Enum.TryParse<FactoryIdentity>(fabryka, out FactoryIdentity fabrykaEnum))
            {
                return new ResultWithMessage(false, "Wybierz poprawną fabrykę.");
            }

            if (string.IsNullOrWhiteSpace(moc) || !int.TryParse(moc.Trim(), out int mocWartosc) || mocWartosc <= 0)
            {
                return new ResultWithMessage(false, "Moc musi być liczbą całkowitą dodatnią.");
            }

            int pojemnoscWartosc = 0;
            if (silnikEnum == EngineType.Elektryk)
            {
                if (!string.IsNullOrWhiteSpace(pojemnosc) && (!int.TryParse(pojemnosc.Trim(), out pojemnoscWartosc) || pojemnoscWartosc < 0))
                {
                    return new ResultWithMessage(false, "Pojemność musi być liczbą całkowitą nieujemną.");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(pojemnosc) || !int.TryParse(pojemnosc.Trim(), out pojemnoscWartosc) || pojemnoscWartosc < 0)
                {
                    return new ResultWithMessage(false, "Pojemność musi być liczbą całkowitą nieujemną.");
                }
            }

            if (string.IsNullOrWhiteSpace(koszt))
            {
                return new ResultWithMessage(false, "Koszt zamówienia nie może być pusty.");
            }

            string kosztClean = koszt.Trim().Replace('.', ',');
            if (!decimal.TryParse(kosztClean, NumberStyles.Number, CultureInfo.GetCultureInfo("pl-PL"), out decimal kosztWartosc) || kosztWartosc < 0)
            {
                return new ResultWithMessage(false, "Koszt musi być liczbą nieujemną.");
            }

            DateTime data = dataZamowienia ?? DateTime.Now;

            try
            {
                bool czyZamowienieIstnieje = await datacontext.Zamowienia.AnyAsync(z => z.Vin_pojazdu == vinNumer);
                if (czyZamowienieIstnieje)
                {
                    return new ResultWithMessage(false, $"Pojazd o numerze VIN {vinNumer} ma już przypisane zamówienie.");
                }

                Pojazd? pojazd = await datacontext.Pojazdy.FirstOrDefaultAsync(p => p.Vin_pojazdu == vinNumer);
                if (pojazd == null)
                {
                    pojazd = new Pojazd
                    {
                        Vin_pojazdu = vinNumer,
                        Model = $"{marka.Trim()} {model.Trim()}",
                        Typ_silnika = silnikEnum,
                        Moc = mocWartosc,
                        Pojemnosc = pojemnoscWartosc,
                        Fabryka = fabrykaEnum
                    };
                    await datacontext.Pojazdy.AddAsync(pojazd);
                }

                Klient? klient = await datacontext.Klienci.FirstOrDefaultAsync(k => k.Pesel == peselTrimmed);
                if (klient == null)
                {
                    klient = new Klient
                    {
                        Id = Guid.NewGuid(),
                        Imie = imie.Trim(),
                        Nazwisko = nazwisko.Trim(),
                        Pesel = peselTrimmed
                    };
                    await datacontext.Klienci.AddAsync(klient);
                }

                Zamowienie zamowienie = new Zamowienie
                {
                    Id_zamowienia = Guid.NewGuid(),
                    Data = data,
                    KlientId = klient.Id,
                    Klient = klient,
                    Vin_pojazdu = vinNumer,
                    Pojazd = pojazd,
                    Koszt = (int)Math.Round(kosztWartosc)
                };
                await datacontext.Zamowienia.AddAsync(zamowienie);

                await datacontext.SaveChangesAsync();

                return new ResultWithMessage(true, "Pomyślnie dodano zamówienie do bazy danych.");
            }
            catch (Exception ex)
            {
                return new ResultWithMessage(false, $"Błąd podczas dodawania zamówienia do bazy danych: {ex.Message}");
            }
        }
        #endregion
    }
}
