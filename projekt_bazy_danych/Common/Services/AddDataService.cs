using projekt_bazy_danych.Common.Repositories;
using projekt_bazy_danych.Common.Utils;
using projekt_bazy_danych.Entities;

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

            Klient klient = new Klient(imie, nazwisko, pesel);
            ResultWithMessage result = await klientRepository.AddKlient(klient);

            if (!result.IsValid)
            {
                return result;
            }

            return new ResultWithMessage(true);
        }
        #endregion
    }
}
