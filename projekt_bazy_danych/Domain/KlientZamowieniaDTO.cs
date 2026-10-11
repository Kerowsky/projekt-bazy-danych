namespace projekt_bazy_danych.Domain
{
    public sealed class KlientZamowieniaDTO
    {
        #region Properties
        public Guid Id { get; set; }

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public List<ZamowienieDTO> Zamowienia { get; set; }

        #endregion

        #region
        public KlientZamowieniaDTO()
        {
        }
        #endregion
    }
}
