namespace projekt_bazy_danych.Domain.DTOs
{
    public sealed class KlientZamowieniaDTO
    {
        #region Properties
        public KlientDTO Klient { get; set; }

        public List<ZamowienieDTO> Zamowienia { get; set; }

        #endregion

        #region
        public KlientZamowieniaDTO()
        {
        }
        #endregion
    }
}
