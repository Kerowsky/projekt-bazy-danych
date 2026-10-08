using projekt_bazy_danych.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace projekt_bazy_danych.Entities
{
    class Pojazd
    {
        [Key]
        public int Vin_pojazdu { get; set; }

        public string Model { get; set; }

        public EngineType Typ_silnika { get; set; }

        public int Moc { get; set; }

        public int Pojemnosc { get; set; }

        public FactoryIdentity Fabryka { get; set; }
    }
}
