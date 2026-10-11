using System.ComponentModel.DataAnnotations;

namespace projekt_bazy_danych.Entities
{
    public sealed class Zamowienie
    {
        [Key]
        public Guid Id_zamowienia { get; set; }

        public DateTime Data { get; set; }

        public Guid KlientId { get; set; }

        public int Vin_pojazdu { get; set; }

        public int Koszt { get; set; }

        public Klient? Klient { get; set; }

        public Pojazd? Pojazd { get; set; }
    }
}
