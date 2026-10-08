using System.ComponentModel.DataAnnotations;

namespace projekt_bazy_danych.Entities
{
    class Zamowienie
    {
        [Key]
        public Guid Id_zamowienia { get; set; }

        public DateTime Data { get; set; }

        public int Vin_pojazdu { get; set; }

        public int Koszt { get; set; }
    }
}
