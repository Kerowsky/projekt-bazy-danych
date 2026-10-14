namespace projekt_bazy_danych.Domain.DTOs
{
    public sealed class ZamowienieDTO
    {
        #region
        public Guid Id_zamowienia { get; set; }

        public DateTime Data { get; set; }

        public int Vin_pojazdu { get; set; }

        public int Koszt { get; set; }
        #endregion
    }
}
