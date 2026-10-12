namespace projekt_bazy_danych.Entities
{
    public sealed class Klient 
    {
        public Guid Id { get; set; }

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public string Pesel { get; set; }

        public List<Zamowienie> Id_zamowienia { get; set; } = new List<Zamowienie>();

        #region Constructors
        public Klient() { }

        public Klient(string Imie, string Nazwisko, string Pesel)
        {
            this.Id = new Guid();
            this.Imie = Imie;
            this.Nazwisko = Nazwisko;
            this.Pesel = Pesel;
        }
        #endregion
    }
}
