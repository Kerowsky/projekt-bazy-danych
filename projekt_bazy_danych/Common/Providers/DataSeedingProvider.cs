using Newtonsoft.Json;
using projekt_bazy_danych.Entities;

namespace projekt_bazy_danych.Common.Providers
{
    public sealed class DataSeedingProvider
    {
        /// <summary>
        /// Odczytuje dane z Klienci.json (Zhardcodowane)
        /// </summary>
        /// <returns>List<Klienci></returns>
        public static List<Klient> GenerateKlient()
        {
            List<Klient> klienci = new List<Klient>();

            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Klienci.json");
                string json = System.IO.File.ReadAllText(path);
                klienci = JsonConvert.DeserializeObject<List<Klient>>(json);

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
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Pojazdy.json");
                string json = System.IO.File.ReadAllText(path);
                pojazdy = JsonConvert.DeserializeObject<List<Pojazd>>(json);

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
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Zamowienia.json");
                string json = System.IO.File.ReadAllText(path);
                zamowienia = JsonConvert.DeserializeObject<List<Zamowienie>>(json);

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
