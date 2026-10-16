using projekt_bazy_danych.Common.Enums;

namespace projekt_bazy_danych.Domain.DTOs
{
    /// <summary>
    /// Obiekt DTO zawierający szczegółowe dane zamówienia, klienta oraz pojazdu do prezentacji w tabeli głównej.
    /// </summary>
    public sealed class SzczegolyZamowieniaDTO
    {
        public Guid Id_zamowienia { get; set; }

        public DateTime Data { get; set; }

        public int Koszt { get; set; }

        public string Imie { get; set; } = string.Empty;

        public string Nazwisko { get; set; } = string.Empty;

        public string Pesel { get; set; } = string.Empty;

        public int Vin_pojazdu { get; set; }

        public string Marka { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public EngineType Typ_silnika { get; set; }

        public int Moc { get; set; }

        public int Pojemnosc { get; set; }

        public FactoryIdentity Fabryka { get; set; }
    }
}
