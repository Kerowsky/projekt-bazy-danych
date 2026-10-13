using projekt_bazy_danych.Common.Enums;

namespace projekt_bazy_danych.Domain.DTOs
{
    public sealed class PojazdDTO
    {
        public int Vin_pojazdu { get; set; }

        public string Model { get; set; }

        public EngineType Typ_silnika { get; set; }

        public int Moc { get; set; }

        public int Pojemnosc { get; set; }

        public FactoryIdentity Fabryka { get; set; }
    }
}
