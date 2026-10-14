namespace projekt_bazy_danych.Domain.DTOs
{
    public sealed class KlientDTO
    {
        public Guid Id { get; set; }

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public string Pesel { get; set; }


        public KlientDTO()
        {

        }

        public KlientDTO(Guid id, string imie, string nazwisko, string pesel)
        {
            this.Id = id;
            this.Imie = imie;
            this.Nazwisko = nazwisko;
            this.Pesel = pesel;
        }

        public KlientDTO(string imie, string nazwisko, string pesel)
        {
            this.Id = new Guid();
            this.Imie = imie;
            this.Nazwisko = nazwisko;
            this.Pesel = pesel;
        }
    }
}
