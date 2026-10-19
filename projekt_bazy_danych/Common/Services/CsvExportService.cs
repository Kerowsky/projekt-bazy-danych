using projekt_bazy_danych.Common.Utils;
using projekt_bazy_danych.Domain.DTOs;
using System.IO;
using System.Text;

namespace projekt_bazy_danych.Common.Services
{
    /// <summary>
    /// Serwis odpowiedzialny za eksport danych tabelarycznych zamówień do formatu CSV.
    /// </summary>
    public sealed class CsvExportService
    {
        #region Methods
        /// <summary>
        /// Asynchronicznie eksportuje kolekcję zamówień do pliku CSV ze średnikiem jako separatorem i kodowaniem UTF-8 z BOM.
        /// </summary>
        /// <param name="sciezkaPliku">Ścieżka docelowa pliku CSV.</param>
        /// <param name="zamowienia">Kolekcja zamówień do wyeksportowania.</param>
        /// <returns>Obiekt ResultWithMessage informujący o powodzeniu lub błędzie operacji.</returns>
        public async Task<ResultWithMessage> EksportujZamowieniaDoCsv(string sciezkaPliku, IEnumerable<SzczegolyZamowieniaDTO> zamowienia)
        {
            if (string.IsNullOrWhiteSpace(sciezkaPliku))
            {
                return new ResultWithMessage(false, "Nie podano ścieżki pliku docelowego.");
            }

            try
            {
                var sb = new StringBuilder();

                // Nagłówek pliku CSV
                sb.AppendLine("ID zamówienia;Imię;Nazwisko;PESEL;VIN pojazdu;Marka;Model;Typ silnika;Moc (KM);Pojemność (cm³);Fabryka;Koszt (zł);Data zamówienia");

                // Wiersze danych
                int liczbaWierszy = 0;
                foreach (SzczegolyZamowieniaDTO z in zamowienia)
                {
                    string linia = string.Join(";",
                        uciekaWartosc(z.Id_zamowienia.ToString()),
                        uciekaWartosc(z.Imie),
                        uciekaWartosc(z.Nazwisko),
                        uciekaWartosc(z.Pesel),
                        uciekaWartosc(z.Vin_pojazdu.ToString()),
                        uciekaWartosc(z.Marka),
                        uciekaWartosc(z.Model),
                        uciekaWartosc(z.Typ_silnika.ToString()),
                        uciekaWartosc(z.Moc.ToString()),
                        uciekaWartosc(z.Pojemnosc.ToString()),
                        uciekaWartosc(z.Fabryka.ToString()),
                        uciekaWartosc(z.Koszt.ToString()),
                        uciekaWartosc(z.Data.ToString("yyyy-MM-dd HH:mm:ss")));

                    sb.AppendLine(linia);
                    liczbaWierszy++;
                }

                // Zapis z kodowaniem UTF-8 z BOM (zapewnia poprawne otwieranie polskich znaków w programie Excel)
                Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
                await File.WriteAllTextAsync(sciezkaPliku, sb.ToString(), encoding);

                return new ResultWithMessage(true, $"Pomyślnie wyeksportowano {liczbaWierszy} zamówień do pliku:\n{sciezkaPliku}");
            }
            catch (Exception ex)
            {
                return new ResultWithMessage(false, $"Wystąpił błąd podczas zapisu pliku CSV: {ex.Message}");
            }
        }

        /// <summary>
        /// Zabezpiecza wartość tekstową przed separatorami i znakami nowej linii w formacie CSV.
        /// </summary>
        /// <param name="wartosc">Wartość do zabezpieczenia.</param>
        /// <returns>Zabezpieczona wartość tekstowa.</returns>
        private static string uciekaWartosc(string? wartosc)
        {
            if (string.IsNullOrEmpty(wartosc))
            {
                return string.Empty;
            }

            if (wartosc.Contains(';') || wartosc.Contains('"') || wartosc.Contains('\n') || wartosc.Contains('\r'))
            {
                return $"\"{wartosc.Replace("\"", "\"\"")}\"";
            }

            return wartosc;
        }
        #endregion
    }
}
