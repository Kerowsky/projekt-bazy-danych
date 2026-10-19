using projekt_bazy_danych.Common.Enums;
using projekt_bazy_danych.Common.Providers.Interfaces;
using projekt_bazy_danych.Common.Repositories;
using projekt_bazy_danych.Common.Utils;
using projekt_bazy_danych.Domain.DTOs;
using projekt_bazy_danych.Common;
using projekt_bazy_danych.Entities;
using System.Collections.ObjectModel;

namespace projekt_bazy_danych.Common.Providers
{
    public class DataProvider : IViewDataProvider
    {
        #region Fields
        private readonly Datacontext datacontext;

        private readonly KlientRepository klientRepository;

        private readonly ZamowienieRepository zamowienieRepository;

        private readonly PojazdRepository pojazdRepository;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public DataProvider(
            Datacontext datacontext)
        {
            this.datacontext = datacontext;
            this.klientRepository = new KlientRepository(datacontext);
            this.zamowienieRepository = new ZamowienieRepository(datacontext);
            this.pojazdRepository = new PojazdRepository(datacontext);
        }
        #endregion

        #region Methods
        public async Task<KlientZamowieniaDTO> GetZamowienieKlienta(Guid idKlienta)
        {
            Klient klient = await klientRepository.GetKlientById(idKlienta);
            Zamowienie zamowienie = await zamowienieRepository.GetZamowienieByKlientId(klient.Id);

            KlientZamowieniaDTO klientZamowieniaDTO = new KlientZamowieniaDTO();
            List<ZamowienieDTO> zamowieniaDTO = new List<ZamowienieDTO>();

            //todo sprawdzac czy wszystkie zamowienia sie dopisuja
            zamowieniaDTO.Add(new ZamowienieDTO()
            {
                Id_zamowienia = zamowienie.Id_zamowienia,
                Data = zamowienie.Data,
                Vin_pojazdu = zamowienie.Vin_pojazdu,
                Koszt = zamowienie.Koszt
            });

            KlientDTO klientDTO = new KlientDTO()
            {
                Id = klient.Id,
                Imie = klient.Imie,
                Nazwisko = klient.Nazwisko,
                Pesel = klient.Pesel
            };

            klientZamowieniaDTO= new KlientZamowieniaDTO()
            {
                Klient = klientDTO,
                Zamowienia = zamowieniaDTO
            };

            return klientZamowieniaDTO;
        }

        public async Task<ObservableCollection<ZamowienieDTO>> GetZamowienia()
        {
            IEnumerable<Zamowienie> zamowienia = await zamowienieRepository.GetAllRecords();

            ObservableCollection<ZamowienieDTO> zamowieniaDTO = new ObservableCollection<ZamowienieDTO>();

            foreach (Zamowienie zamowienie in zamowienia)
            {
                zamowieniaDTO.Add(new ZamowienieDTO()
                {
                    Id_zamowienia = zamowienie.Id_zamowienia,
                    Data = zamowienie.Data,
                    Vin_pojazdu = zamowienie.Vin_pojazdu,
                    Koszt = zamowienie.Koszt
                });
            }

            return zamowieniaDTO;
        }

        public async Task<ObservableCollection<PojazdDTO>> GetPojazdy(string factoryIdentity)
        {
            IEnumerable<Pojazd> pojazdy = await pojazdRepository.GetAllRecords();

            ObservableCollection<PojazdDTO> pojazdyDTO = new ObservableCollection<PojazdDTO>();

            foreach (Pojazd pojazd in pojazdy.Where(p => p.Fabryka == factoryIdentity.ToFactoryIdentity()))
            {
                pojazdyDTO.Add(new PojazdDTO()
                {
                    Vin_pojazdu = pojazd.Vin_pojazdu,
                    Model = pojazd.Model,
                    Typ_silnika = pojazd.Typ_silnika,
                    Moc = pojazd.Moc,
                    Pojemnosc = pojazd.Pojemnosc,
                    Fabryka = pojazd.Fabryka
                });
            }

            return pojazdyDTO;
        }

        /// <summary>
        /// Pobiera przefiltrowane zamówienia wraz ze szczegółami klienta i pojazdu.
        /// </summary>
        /// <param name="filtr">Obiekt kryteriów filtrowania.</param>
        /// <returns>Kolekcja obiektów SzczegolyZamowieniaDTO.</returns>
        public async Task<ObservableCollection<SzczegolyZamowieniaDTO>> GetZamowieniaSzczegoly(ZamowienieFiltrDTO? filtr = null)
        {
            List<Zamowienie> zamowienia = await zamowienieRepository.GetZamowieniaFiltered(filtr);

            ObservableCollection<SzczegolyZamowieniaDTO> wynik = new ObservableCollection<SzczegolyZamowieniaDTO>();

            foreach (Zamowienie z in zamowienia)
            {
                var (marka, model) = rozdzielMarkeIModel(z.Pojazd?.Model ?? string.Empty);

                wynik.Add(new SzczegolyZamowieniaDTO
                {
                    Id_zamowienia = z.Id_zamowienia,
                    Data = z.Data,
                    Koszt = z.Koszt,
                    Imie = z.Klient?.Imie ?? string.Empty,
                    Nazwisko = z.Klient?.Nazwisko ?? string.Empty,
                    Pesel = z.Klient?.Pesel ?? string.Empty,
                    Vin_pojazdu = z.Vin_pojazdu,
                    Marka = marka,
                    Model = model,
                    Typ_silnika = z.Pojazd?.Typ_silnika ?? default,
                    Moc = z.Pojazd?.Moc ?? 0,
                    Pojemnosc = z.Pojazd?.Pojemnosc ?? 0,
                    Fabryka = z.Pojazd?.Fabryka ?? default
                });
            }

            return wynik;
        }

        /// <summary>
        /// Pomocnicza metoda rozdzielająca nazwę modelu z bazy na markę i model pojazdu.
        /// </summary>
        /// <param name="nazwa">Pełna nazwa modelu pojazdu.</param>
        /// <returns>Krotka zawierająca markę oraz model.</returns>
        private static (string Marka, string Model) rozdzielMarkeIModel(string nazwa)
        {
            if (string.IsNullOrWhiteSpace(nazwa))
            {
                return (string.Empty, string.Empty);
            }

            string trimmed = nazwa.Trim();
            if (trimmed.StartsWith("Alfa Romeo", StringComparison.OrdinalIgnoreCase))
            {
                return ("Alfa Romeo", trimmed.Substring("Alfa Romeo".Length).Trim());
            }

            int firstSpace = trimmed.IndexOf(' ');
            if (firstSpace > 0)
            {
                return (trimmed.Substring(0, firstSpace), trimmed.Substring(firstSpace + 1).Trim());
            }

            return (trimmed, trimmed);
        }

        /// <summary>
        /// Usuwa zamówienie z bazy danych za pośrednictwem repozytorium zamówień.
        /// </summary>
        /// <param name="idZamowienia">Identyfikator zamówienia do usunięcia.</param>
        /// <returns>Obiekt ResultWithMessage informujący o wyniku operacji.</returns>
        public async Task<ResultWithMessage> UsunZamowienie(Guid idZamowienia)
        {
            return await zamowienieRepository.UsunZamowienie(idZamowienia);
        }
        #endregion
    }
}
