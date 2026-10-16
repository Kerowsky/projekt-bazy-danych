using projekt_bazy_danych.Common.Enums;

namespace projekt_bazy_danych.Domain.DTOs
{
    /// <summary>
    /// Obiekt DTO przekazujący parametry filtrowania zamówień do zapytań Entity Framework.
    /// </summary>
    public sealed class ZamowienieFiltrDTO
    {
        public string? Imie { get; set; }

        public string? Nazwisko { get; set; }

        public int? Vin { get; set; }

        public string? Marka { get; set; }

        public string? Model { get; set; }

        public EngineType? TypSilnika { get; set; }

        public FactoryIdentity? Fabryka { get; set; }

        public int? KosztOd { get; set; }

        public int? KosztDo { get; set; }

        public int? MocOd { get; set; }

        public int? MocDo { get; set; }

        public int? PojemnoscOd { get; set; }

        public int? PojemnoscDo { get; set; }

        public DateTime? DataOd { get; set; }

        public DateTime? DataDo { get; set; }
    }
}
