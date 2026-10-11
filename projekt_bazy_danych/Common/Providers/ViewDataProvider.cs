using projekt_bazy_danych.Common.Providers.Interfaces;
using projekt_bazy_danych.Common.Repositories;
using projekt_bazy_danych.Domain;
using projekt_bazy_danych.Entities;

namespace projekt_bazy_danych.Common.Providers
{
    public class ViewDataProvider : IViewDataProvider
    {
        #region Fields
        private readonly Datacontext datacontext;
        private readonly KlientRepository klientRepository;

        private readonly ZamowienieRepository zamowienieRepository;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public ViewDataProvider(
            Datacontext datacontext)
        {
            this.datacontext = datacontext;
            klientRepository = new KlientRepository(datacontext);
            zamowienieRepository = new ZamowienieRepository(datacontext);
        }
        #endregion

        #region Methods
        public async Task<KlientZamowieniaDTO> GetZamowienieKlienta(Guid idKlienta)
        {
            Klient klient = await klientRepository.GetKlientById(new Guid("10000000-0000-0000-0000-000000000001"));
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
        #endregion
    }
}
