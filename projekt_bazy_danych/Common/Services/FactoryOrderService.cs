using projekt_bazy_danych.Common.Repositories;
using projekt_bazy_danych.Entities;

namespace projekt_bazy_danych.Common.Services
{
    public sealed class FactoryOrderService
    {
        #region Fields
        private readonly Datacontext datacontext;

        private readonly KlientRepository klientRepository;

        private readonly ZamowienieRepository zamowienieRepository;

        private readonly PojazdRepository pojazdRepository;
        #endregion

        #region Constructors
        public FactoryOrderService(Datacontext datacontext)
        {
            this.datacontext = datacontext;
            this.klientRepository = new KlientRepository(datacontext);
            this.zamowienieRepository = new ZamowienieRepository(datacontext);
            this.pojazdRepository = new PojazdRepository(datacontext);
        }
        #endregion

        #region Methods
        public async Task<bool> AddNewKlient(string Imie, string Nazwisko, string Pesel)
        {
            Klient klient = new Klient(Imie, Nazwisko, Pesel);



            return true;

        }
        #endregion
    }
}
