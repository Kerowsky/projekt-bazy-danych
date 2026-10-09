using Newtonsoft.Json;
using projekt_bazy_danych.Entities;

namespace projekt_bazy_danych.Common.Providers
{
    public sealed class DataSeedingProvider
    {
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
    }
}
