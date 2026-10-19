using System.IO;
using Newtonsoft.Json;
using projekt_bazy_danych.Entities;

namespace projekt_bazy_danych.Common.Providers
{
    public sealed class DataSeedingProvider
    {
        /// <summary>
        /// Zwraca bezwzględną ścieżkę do pliku zasobu, sprawdzając lokalizację binarną oraz katalog projektu.
        /// </summary>
        /// <param name="nazwaPliku">Nazwa pliku zasobu JSON.</param>
        /// <returns>Ścieżka do istniejącego pliku zasobu.</returns>
        private static string pobierzSciezkeZasobu(string nazwaPliku)
        {
            string sciezkaBazy = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", nazwaPliku);
            if (File.Exists(sciezkaBazy))
            {
                return sciezkaBazy;
            }

            string sciezkaKataloguBiezacego = Path.Combine(Directory.GetCurrentDirectory(), "Resources", nazwaPliku);
            if (File.Exists(sciezkaKataloguBiezacego))
            {
                return sciezkaKataloguBiezacego;
            }

            string sciezkaKataloguProjektu = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources", nazwaPliku);
            if (File.Exists(sciezkaKataloguProjektu))
            {
                return Path.GetFullPath(sciezkaKataloguProjektu);
            }

            return sciezkaBazy;
        }

        /// <summary>
        /// Odczytuje dane z Klienci.json (Zhardcodowane)
        /// </summary>
        /// <returns>List<Klient></returns>
        public static List<Klient> GenerateKlient()
        {
            List<Klient> klienci = new List<Klient>();

            try
            {
                string path = pobierzSciezkeZasobu("Klienci.json");
                string json = System.IO.File.ReadAllText(path);
                klienci = JsonConvert.DeserializeObject<List<Klient>>(json) ?? new List<Klient>();

                return klienci;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wyjątek " + ex);

                return klienci;
            }
        }

        /// <summary>
        /// Odczytuje dane z Pojazdy.json (Zhardcodowane)
        /// </summary>
        /// <returns>List<Pojazdy></returns>
        public static List<Pojazd> GeneratePojazd()
        {
            List<Pojazd> pojazdy = new List<Pojazd>();

            try
            {
                string path = pobierzSciezkeZasobu("Pojazdy.json");
                string json = System.IO.File.ReadAllText(path);
                pojazdy = JsonConvert.DeserializeObject<List<Pojazd>>(json) ?? new List<Pojazd>();

                return pojazdy;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wyjątek " + ex);

                return pojazdy;
            }
        }

        /// <summary>
        /// Odczytuje dane z Zamowienia.json (Zhardcodowane)
        /// </summary>
        /// <returns>List<Zamowienia></returns>
        public static List<Zamowienie> GenerateZamowienia()
        {
            List<Zamowienie> zamowienia = new List<Zamowienie>();

            try
            {
                string path = pobierzSciezkeZasobu("Zamowienia.json");
                string json = System.IO.File.ReadAllText(path);
                zamowienia = JsonConvert.DeserializeObject<List<Zamowienie>>(json) ?? new List<Zamowienie>();

                return zamowienia;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wyjątek " + ex);

                return zamowienia;
            }
        }
    }
}
