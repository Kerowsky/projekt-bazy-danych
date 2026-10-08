namespace projekt_bazy_danych.Entities
{
    class Klient 
    {
        public Guid Id { get; set; }

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public List<Zamowienie> Id_zamowienia { get; set; } = new List<Zamowienie>();
    }
}
