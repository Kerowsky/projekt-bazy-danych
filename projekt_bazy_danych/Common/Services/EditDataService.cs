using Microsoft.EntityFrameworkCore;
using projekt_bazy_danych.Common.Enums;
using projekt_bazy_danych.Common.Utils;
using projekt_bazy_danych.Entities;
using System.Globalization;

namespace projekt_bazy_danych.Common.Services
{
    /// <summary>
    /// Serwis odpowiedzialny za aktualizację i edycję danych zamówień, klientów oraz pojazdów w bazie danych.
    /// </summary>
    public sealed class EditDataService
    {
        #region Fields
        private readonly Datacontext datacontext;
        #endregion

        #region Constructors
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="EditDataService"/>.
        /// </summary>
        /// <param name="datacontext">Kontekst bazy danych Entity Framework.</param>
        public EditDataService(Datacontext datacontext)
        {
            this.datacontext = datacontext;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Aktualizuje dane istniejącego zamówienia, klienta oraz pojazdu w bazie danych.
        /// </summary>
        /// <param name="idZamowienia">Identyfikator zamówienia do edycji.</param>
        /// <param name="imie">Nowe imię klienta.</param>
        /// <param name="nazwisko">Nowe nazwisko klienta.</param>
        /// <param name="pesel">Nowy numer PESEL klienta.</param>
        /// <param name="marka">Nowa marka pojazdu.</param>
        /// <param name="model">Nowy model pojazdu.</param>
        /// <param name="typSilnika">Nowy typ silnika pojazdu.</param>
        /// <param name="fabryka">Nowa fabryka pojazdu.</param>
        /// <param name="moc">Nowa moc pojazdu w KM.</param>
        /// <param name="pojemnosc">Nowa pojemność silnika w cm³.</param>
        /// <param name="koszt">Nowy koszt zamówienia w PLN.</param>
        /// <param name="dataZamowienia">Nowa data zamówienia.</param>
        /// <returns>Obiekt ResultWithMessage informujący o powodzeniu lub błędzie operacji.</returns>
        public async Task<ResultWithMessage> EditZamowienie(
            Guid idZamowienia,
            string? imie,
            string? nazwisko,
            string? pesel,
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
                Zamowienie? zamowienie = await datacontext.Zamowienia
                    .Include(z => z.Klient)
                    .Include(z => z.Pojazd)
                    .FirstOrDefaultAsync(z => z.Id_zamowienia == idZamowienia);

                if (zamowienie == null)
                {
                    return new ResultWithMessage(false, "Nie odnaleziono zamówienia w bazie danych.");
                }

                zamowienie.Data = data;
                zamowienie.Koszt = (int)Math.Round(kosztWartosc);

                if (zamowienie.Klient != null)
                {
                    zamowienie.Klient.Imie = imie.Trim();
                    zamowienie.Klient.Nazwisko = nazwisko.Trim();
                    zamowienie.Klient.Pesel = peselTrimmed;
                }

                if (zamowienie.Pojazd != null)
                {
                    zamowienie.Pojazd.Model = $"{marka.Trim()} {model.Trim()}";
                    zamowienie.Pojazd.Typ_silnika = silnikEnum;
                    zamowienie.Pojazd.Fabryka = fabrykaEnum;
                    zamowienie.Pojazd.Moc = mocWartosc;
                    zamowienie.Pojazd.Pojemnosc = pojemnoscWartosc;
                }

                await datacontext.SaveChangesAsync();

                return new ResultWithMessage(true, "Pomyślnie zaktualizowano zamówienie.");
            }
            catch (Exception ex)
            {
                return new ResultWithMessage(false, $"Błąd podczas aktualizacji zamówienia: {ex.Message}");
            }
        }
        #endregion
    }
}
