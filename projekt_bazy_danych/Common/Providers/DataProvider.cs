using projekt_bazy_danych.Common.Enums;
using projekt_bazy_danych.Common.Providers.Interfaces;
using projekt_bazy_danych.Common.Repositories;
using projekt_bazy_danych.Domain.DTOs;
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

            klientZamowieniaDTO= new KlientZamowieniaDTO()
            {
                Id = klient.Id,
                Imie = klient.Imie,
                Nazwisko = klient.Nazwisko,
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
        #endregion
    }
}
