using projekt_bazy_danych.Common.Enums;
using projekt_bazy_danych.Domain.DTOs;
using ScottPlot;
using ScottPlot.WPF;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace projekt_bazy_danych
{
    /// <summary>
    /// Logika okna wykresów. Wykresy są budowane na podstawie przefiltrowanych danych
    /// przekazanych przez <see cref="IReadOnlyList{SzczegolyZamowieniaDTO}"/>.
    /// </summary>
    public partial class ChartWindow : Window
    {
        private readonly IReadOnlyList<SzczegolyZamowieniaDTO> dane;

        /// <summary>
        /// Inicjalizuje okno wykresów z podanym zestawem przefiltrowanych zamówień.
        /// </summary>
        /// <param name="dane">Przefiltrowane zamówienia z głównego widoku.</param>
        public ChartWindow(IReadOnlyList<SzczegolyZamowieniaDTO> dane)
        {
            InitializeComponent();
            this.dane = dane;

            przypiszHandlery();
        }

        // ─────────────────────────────────────────────────────────────
        //  Rejestracja zdarzeń przycisków
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Przypisuje handlery kliknięcia do wszystkich przycisków wykresu.
        /// </summary>
        private void przypiszHandlery()
        {
            BtnPrzychodModel.Click += (_, _) => rysujPrzychodWgModelu();
            BtnPrzychodMarka.Click += (_, _) => rysujSlupkowe("Przychód wg marki",
                dane.GroupBy(d => d.Marka).Select(g => (g.Key, (double)g.Sum(x => x.Koszt))));
            BtnPrzychodFabryka.Click += (_, _) => rysujSlupkowe("Przychód wg fabryki",
                dane.GroupBy(d => d.Fabryka.ToString()).Select(g => (g.Key, (double)g.Sum(x => x.Koszt))));
            BtnZamowieniaMBF.Click += (_, _) => pokazDialogZamowieniaMBF();
            BtnSredniKosztMBT.Click += (_, _) => pokazDialogSredniKosztMBT();
            BtnTopKlienci.Click += (_, _) => rysujTopKlienci();
            BtnSredniaMoc.Click += (_, _) => rysujSlupkowe("Śr. moc (KM) wg marki",
                dane.GroupBy(d => d.Marka).Select(g => (g.Key, g.Average(x => (double)x.Moc))));

            BtnPrzychodMiesiac.Click += (_, _) => pokazDialogPrzychodOkres();
            BtnZamowieniaMiesiac.Click += (_, _) => rysujZamowieniaMiesiecznie();
            BtnPrzychodKumulatywny.Click += (_, _) => rysujPrzychodKumulatywny();

            BtnUdzialSilnik.Click += (_, _) => rysujKolowe("Udział typów silnika",
                dane.GroupBy(d => d.Typ_silnika.ToString()).Select(g => (g.Key, (double)g.Count())));
            BtnUdzialFabryka.Click += (_, _) => rysujKolowe("Udział fabryk w przychodzie",
                dane.GroupBy(d => d.Fabryka.ToString()).Select(g => (g.Key, (double)g.Sum(x => x.Koszt))));
            BtnUdzialMarka.Click += (_, _) => rysujKolowe("Udział marek w zamówieniach",
                dane.GroupBy(d => d.Marka).Select(g => (g.Key, (double)g.Count())));

            BtnPrzychodMarkaSilnik.Click += (_, _) => rysujPrzychodMarkaSilnik();
            BtnPrzychodFabrykaMarki.Click += (_, _) => rysujPrzychodFabrykaMarki();
            BtnZamowieniaFabryki.Click += (_, _) => rysujZamowieniaFabryki();

            BtnMocKoszt.Click += (_, _) => rysujScatterMoc();
            BtnPojemnoscKoszt.Click += (_, _) => rysujScatterPojemnosc();

            BtnHistMoc.Click += (_, _) => rysujHistogram("Rozkład mocy (KM)", dane.Select(d => (double)d.Moc).ToArray(), 20, "Moc [KM]", "Liczba zamówień");
        }

        // ─────────────────────────────────────────────────────────────
        //  Pomocnicze – reset i tytuł
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Czyści wykres, resetuje konfigurację osi, marginesów i legendy do domyślnych wartości
        /// i ustawia nowy tytuł w nagłówku okna.
        /// </summary>
        private void resetujWykres(string tytul)
        {
            WpfPlotChart.Plot.Clear();

            // Ukrycie legendy
            WpfPlotChart.Plot.Legend.IsVisible = false;

            // Reset osi X
            WpfPlotChart.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericAutomatic();
            WpfPlotChart.Plot.Axes.Bottom.TickLabelStyle.Rotation = 0;
            WpfPlotChart.Plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.UpperCenter;
            WpfPlotChart.Plot.Axes.Bottom.MinimumSize = 0;
            WpfPlotChart.Plot.Axes.Bottom.Label.Text = string.Empty;

            // Reset osi Y
            WpfPlotChart.Plot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericAutomatic();
            WpfPlotChart.Plot.Axes.Left.TickLabelStyle.Rotation = 0;
            WpfPlotChart.Plot.Axes.Left.MinimumSize = 0;
            WpfPlotChart.Plot.Axes.Left.Label.Text = string.Empty;

            // Reset marginesów i reguł osi
            WpfPlotChart.Plot.Axes.Margins(0.05, 0.05, 0.05, 0.05);
            WpfPlotChart.Plot.Axes.Rules.Clear();

            ChartTitle.Text = tytul;
        }

        /// <summary>
        /// Odświeża i renderuje wykres po wprowadzeniu zmian.
        /// </summary>
        private void odswiezWykres()
        {
            WpfPlotChart.Plot.Axes.AutoScale();
            WpfPlotChart.Refresh();
        }

        /// <summary>
        /// Stosuje obrót etykiet osi X o -45°, wyrównanie MiddleRight oraz minimalny rozmiar panelu
        /// osi tak, aby etykiety nie nakładały się i nie były ucinane.
        /// Należy wywołać po ustawieniu TickGenerator, a przed odswiezWykres().
        /// </summary>
        private void aplikujRotacjeOsiX()
        {
            WpfPlotChart.Plot.Axes.Bottom.TickLabelStyle.Rotation = -45;
            WpfPlotChart.Plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.MiddleRight;
            WpfPlotChart.Plot.Axes.Bottom.MinimumSize = 120;
        }

        // ─────────────────────────────────────────────────────────────
        //  Wykresy słupkowe – ogólne
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Rysuje prosty wykres słupkowy na podstawie podanych grup (etykieta, wartość).
        /// </summary>
        private void rysujSlupkowe(string tytul, IEnumerable<(string etykieta, double wartosc)> grupy)
        {
            var lista = grupy.OrderByDescending(g => g.wartosc).ToList();
            if (!sprawdzDane(lista.Count)) return;

            resetujWykres(tytul);

            var bars = lista.Select((g, i) => new Bar { Position = i, Value = g.wartosc, FillColor = paleta(i), Size = 0.7 }).ToArray();
            WpfPlotChart.Plot.Add.Bars(bars);
            WpfPlotChart.Plot.Axes.Margins(bottom: 0);

            var ticks = lista.Select((g, i) => new Tick(i, g.etykieta)).ToArray();
            WpfPlotChart.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
            aplikujRotacjeOsiX();

            odswiezWykres();
        }

        /// <summary>
        /// Rysuje wykres słupkowy przychodu wg marki + modelu (etykieta łączona).
        /// Deleguje do rysujSlupkowe z kluczem złożonym Marka+Model.
        /// </summary>
        private void rysujPrzychodWgModelu()
        {
            var grupy = dane
                .GroupBy(d => $"{d.Marka} {d.Model}")
                .Select(g => (g.Key, (double)g.Sum(x => x.Koszt)));

            rysujSlupkowe("Przychód wg modelu (Marka + Model)", grupy);
        }

        // ─────────────────────────────────────────────────────────────
        //  Top 10 klientów
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Rysuje wykres słupkowy top 10 klientów wg sumarycznych wydatków.
        /// </summary>
        private void rysujTopKlienci()
        {
            var lista = dane
                .GroupBy(d => $"{d.Imie} {d.Nazwisko}")
                .Select(g => (etykieta: g.Key, wartosc: (double)g.Sum(x => x.Koszt)))
                .OrderByDescending(g => g.wartosc)
                .Take(10)
                .ToList();

            if (!sprawdzDane(lista.Count)) return;
            rysujSlupkowe("Top 10 klientów wg wydatków", lista);
        }

        // ─────────────────────────────────────────────────────────────
        //  Wykresy liniowe
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Wyświetla dialog wyboru agregacji przychodu (miesięcznie lub kwartalnie).
        /// </summary>
        private void pokazDialogPrzychodOkres()
        {
            var wybor = pokazDialogWyboru("Przychód w czasie...", new[] { "Miesięcznie", "Kwartalnie" });
            if (wybor == null) return;

            if (wybor == "Kwartalnie")
            {
                rysujPrzychodKwartalnie();
            }
            else
            {
                rysujPrzychodMiesiecznie();
            }
        }

        /// <summary>
        /// Rysuje liniowy wykres przychodu kwartalnie z punktami pomiarowymi.
        /// </summary>
        private void rysujPrzychodKwartalnie()
        {
            var lista = dane
                .GroupBy(d => new { d.Data.Year, Kwartal = (d.Data.Month - 1) / 3 + 1 })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Kwartal)
                .Select(g => (etykieta: $"{g.Key.Kwartal} kw. {g.Key.Year}", wartosc: (double)g.Sum(x => x.Koszt)))
                .ToList();

            if (!sprawdzDane(lista.Count)) return;
            resetujWykres("Przychód kwartalnie");

            var xs = Enumerable.Range(0, lista.Count).Select(i => (double)i).ToArray();
            var ys = lista.Select(g => g.wartosc).ToArray();
            var sc = WpfPlotChart.Plot.Add.Scatter(xs, ys);
            sc.Color = paleta(0);
            sc.LineWidth = 2;
            sc.MarkerSize = 7;

            var ticks = lista.Select((g, i) => new Tick(i, g.etykieta)).ToArray();
            WpfPlotChart.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
            aplikujRotacjeOsiX();

            WpfPlotChart.Plot.Axes.Bottom.Label.Text = "Kwartał";
            WpfPlotChart.Plot.Axes.Left.Label.Text = "Przychód [zł]";

            odswiezWykres();
        }

        /// <summary>
        /// Rysuje liniowy wykres przychodu miesięcznie z punktami pomiarowymi.
        /// </summary>
        private void rysujPrzychodMiesiecznie()
        {
            var lista = dane
                .GroupBy(d => new { d.Data.Year, d.Data.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => (etykieta: $"{g.Key.Month:D2}/{g.Key.Year}", wartosc: (double)g.Sum(x => x.Koszt)))
                .ToList();

            if (!sprawdzDane(lista.Count)) return;
            resetujWykres("Przychód miesięcznie");

            var xs = Enumerable.Range(0, lista.Count).Select(i => (double)i).ToArray();
            var ys = lista.Select(g => g.wartosc).ToArray();
            var sc = WpfPlotChart.Plot.Add.Scatter(xs, ys);
            sc.Color = paleta(0);
            sc.LineWidth = 2;
            sc.MarkerSize = 7;

            var ticks = lista.Select((g, i) => new Tick(i, g.etykieta)).ToArray();
            WpfPlotChart.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
            aplikujRotacjeOsiX();

            WpfPlotChart.Plot.Axes.Bottom.Label.Text = "Miesiąc";
            WpfPlotChart.Plot.Axes.Left.Label.Text = "Przychód [zł]";

            odswiezWykres();
        }

        /// <summary>
        /// Rysuje liniowy wykres liczby zamówień miesięcznie z punktami pomiarowymi.
        /// </summary>
        private void rysujZamowieniaMiesiecznie()
        {
            var lista = dane
                .GroupBy(d => new { d.Data.Year, d.Data.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => (etykieta: $"{g.Key.Month:D2}/{g.Key.Year}", wartosc: (double)g.Count()))
                .ToList();

            if (!sprawdzDane(lista.Count)) return;
            resetujWykres("Liczba zamówień miesięcznie");

            var xs = Enumerable.Range(0, lista.Count).Select(i => (double)i).ToArray();
            var ys = lista.Select(g => g.wartosc).ToArray();
            var sc = WpfPlotChart.Plot.Add.Scatter(xs, ys);
            sc.Color = paleta(1);
            sc.LineWidth = 2;
            sc.MarkerSize = 7;

            var ticks = lista.Select((g, i) => new Tick(i, g.etykieta)).ToArray();
            WpfPlotChart.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
            aplikujRotacjeOsiX();

            WpfPlotChart.Plot.Axes.Bottom.Label.Text = "Miesiąc";
            WpfPlotChart.Plot.Axes.Left.Label.Text = "Liczba zamówień";

            odswiezWykres();
        }

        /// <summary>
        /// Rysuje liniowy wykres przychodu narastającego (kumulatywnego) w ujęciu miesięcznym.
        /// </summary>
        private void rysujPrzychodKumulatywny()
        {
            var miesieczne = dane
                .GroupBy(d => new { d.Data.Year, d.Data.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => (etykieta: $"{g.Key.Month:D2}/{g.Key.Year}", sumaMiesiaca: (double)g.Sum(x => x.Koszt)))
                .ToList();

            if (!sprawdzDane(miesieczne.Count)) return;
            resetujWykres("Przychód narastająco (kumulatywny)");

            double suma = 0;
            var lista = miesieczne.Select(m =>
            {
                suma += m.sumaMiesiaca;
                return (m.etykieta, wartosc: suma);
            }).ToList();

            var xs = Enumerable.Range(0, lista.Count).Select(i => (double)i).ToArray();
            var ys = lista.Select(g => g.wartosc).ToArray();
            var sc = WpfPlotChart.Plot.Add.Scatter(xs, ys);
            sc.Color = paleta(2);
            sc.LineWidth = 2;
            sc.MarkerSize = 7;

            var ticks = lista.Select((g, i) => new Tick(i, g.etykieta)).ToArray();
            WpfPlotChart.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
            aplikujRotacjeOsiX();

            WpfPlotChart.Plot.Axes.Bottom.Label.Text = "Miesiąc";
            WpfPlotChart.Plot.Axes.Left.Label.Text = "Przychód narastająco [zł]";

            odswiezWykres();
        }

        // ─────────────────────────────────────────────────────────────
        //  Wykresy kołowe
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Rysuje wykres kołowy (pie) z podanych grup.
        /// </summary>
        private void rysujKolowe(string tytul, IEnumerable<(string etykieta, double wartosc)> grupy)
        {
            var lista = grupy.Where(g => g.wartosc > 0).ToList();
            if (!sprawdzDane(lista.Count)) return;

            resetujWykres(tytul);

            var pie = WpfPlotChart.Plot.Add.Pie(lista.Select(g => g.wartosc).ToArray());
            for (int i = 0; i < pie.Slices.Count; i++)
            {
                pie.Slices[i].Label = lista[i].etykieta;
                pie.Slices[i].FillColor = paleta(i);
            }
            pie.ExplodeFraction = 0.05;
            WpfPlotChart.Plot.ShowLegend();

            odswiezWykres();
        }

        // ─────────────────────────────────────────────────────────────
        //  Skumulowane / grupowane
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Rysuje zgrupowany wykres słupkowy: przychód wg marki × typ silnika.
        /// </summary>
        private void rysujPrzychodMarkaSilnik()
        {
            rysujGrupowane(
                tytul: "Przychód wg marki × typ silnika",
                klucz1: d => d.Marka,
                klucz2: d => d.Typ_silnika.ToString(),
                agregat: g => (double)g.Sum(x => x.Koszt));
        }

        /// <summary>
        /// Rysuje zgrupowany wykres słupkowy: przychód wg fabryki × marka.
        /// </summary>
        private void rysujPrzychodFabrykaMarki()
        {
            rysujGrupowane(
                tytul: "Przychód wg fabryki × marka",
                klucz1: d => d.Fabryka.ToString(),
                klucz2: d => d.Marka,
                agregat: g => (double)g.Sum(x => x.Koszt));
        }

        /// <summary>
        /// Rysuje zgrupowany wykres słupkowy: zamówienia miesięcznie × fabryki.
        /// </summary>
        private void rysujZamowieniaFabryki()
        {
            rysujGrupowane(
                tytul: "Zamówienia miesięcznie × fabryki",
                klucz1: d => $"{d.Data.Month:D2}/{d.Data.Year}",
                klucz2: d => d.Fabryka.ToString(),
                agregat: g => (double)g.Count());
        }

        /// <summary>
        /// Ogólna metoda rysująca grupowany wykres słupkowy (2 wymiary grupowania).
        /// </summary>
        private void rysujGrupowane(
            string tytul,
            Func<SzczegolyZamowieniaDTO, string> klucz1,
            Func<SzczegolyZamowieniaDTO, string> klucz2,
            Func<IGrouping<string, SzczegolyZamowieniaDTO>, double> agregat)
        {
            var grupy1 = dane.Select(klucz1).Distinct().OrderBy(x => x).ToList();
            var grupy2 = dane.Select(klucz2).Distinct().OrderBy(x => x).ToList();

            if (!sprawdzDane(grupy1.Count)) return;
            resetujWykres(tytul);

            double szerokosc = 0.8 / grupy2.Count;

            for (int k2i = 0; k2i < grupy2.Count; k2i++)
            {
                var k2 = grupy2[k2i];
                var wartosci = grupy1.Select(k1 =>
                {
                    var podgrupa = dane
                        .Where(d => klucz1(d) == k1 && klucz2(d) == k2)
                        .GroupBy(d => klucz2(d))
                        .FirstOrDefault();
                    return podgrupa != null ? agregat(podgrupa) : 0.0;
                }).ToList();

                var bars = wartosci.Select((v, i) => new Bar
                {
                    Position = i + k2i * szerokosc - 0.4 + szerokosc / 2,
                    Value = v,
                    FillColor = paleta(k2i),
                    Size = szerokosc * 0.9
                }).ToArray();

                var barSeries = WpfPlotChart.Plot.Add.Bars(bars);
                barSeries.LegendText = k2;
            }

            WpfPlotChart.Plot.Axes.Margins(bottom: 0);
            var ticks = grupy1.Select((k, i) => new Tick(i, k)).ToArray();
            WpfPlotChart.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
            aplikujRotacjeOsiX();
            WpfPlotChart.Plot.ShowLegend();

            odswiezWykres();
        }

        // ─────────────────────────────────────────────────────────────
        //  Scatter
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Rysuje wykres punktowy mocy silnika vs koszt zamówienia dla modeli pojazdów.
        /// </summary>
        private void rysujScatterMoc()
        {
            rysujScatterModeli(
                tytul: "Moc vs koszt (czy droższe = mocniejsze)",
                filtr: _ => true,
                pobierzX: d => d.Moc,
                labelX: "Moc [KM]",
                jednostkaX: "KM");
        }

        /// <summary>
        /// Rysuje wykres punktowy pojemności silnika vs koszt dla modeli pojazdów (wykluczając pojazdy elektryczne).
        /// </summary>
        private void rysujScatterPojemnosc()
        {
            rysujScatterModeli(
                tytul: "Pojemność vs koszt (bez EV)",
                filtr: d => d.Pojemnosc > 0,
                pobierzX: d => d.Pojemnosc,
                labelX: "Pojemność [cm³]",
                jednostkaX: "cm³");
        }

        /// <summary>
        /// Rysuje wykres punktowy (scatter) dla modeli i wersji silnikowych pojazdów: cecha vs średni koszt.
        /// Grupuje zamówienia wg modelu i wersji silnika, koloruje punkty wg marki, dodaje etykiety modeli
        /// oraz przerywaną linię trendu liniowego.
        /// </summary>
        private void rysujScatterModeli(
            string tytul,
            Func<SzczegolyZamowieniaDTO, bool> filtr,
            Func<SzczegolyZamowieniaDTO, double> pobierzX,
            string labelX,
            string jednostkaX)
        {
            var filtrowane = dane.Where(filtr).ToList();
            if (!sprawdzDane(filtrowane.Count)) return;

            resetujWykres(tytul);

            var grupy = filtrowane
                .GroupBy(d => new { d.Marka, d.Model, d.Typ_silnika, WartoscX = pobierzX(d) })
                .Select(g => new
                {
                    Marka = g.Key.Marka,
                    ModelKrotki = g.Key.Model,
                    TypSilnika = g.Key.Typ_silnika,
                    X = g.Key.WartoscX,
                    Y = g.Average(x => (double)x.Koszt)
                })
                .OrderBy(g => g.X)
                .ToList();

            if (!sprawdzDane(grupy.Count)) return;

            var marki = grupy.Select(g => g.Marka).Distinct().OrderBy(x => x).ToList();
            for (int i = 0; i < marki.Count; i++)
            {
                string marka = marki[i];
                var punktyMarki = grupy.Where(g => g.Marka == marka).ToList();
                var xs = punktyMarki.Select(p => p.X).ToArray();
                var ys = punktyMarki.Select(p => p.Y).ToArray();

                var sc = WpfPlotChart.Plot.Add.ScatterPoints(xs, ys);
                sc.Color = paleta(i);
                sc.MarkerSize = 10;
                sc.LegendText = marka;
            }

            foreach (var p in grupy)
            {
                var txt = WpfPlotChart.Plot.Add.Text(
                    $"{p.ModelKrotki} ({p.X:0} {jednostkaX}, {p.Y / 1000:0} tys. zł)",
                    p.X, p.Y);
                txt.LabelFontSize = 10;
                txt.LabelAlignment = Alignment.LowerCenter;
            }

            if (grupy.Count >= 2)
            {
                var allXs = grupy.Select(p => p.X).ToArray();
                var allYs = grupy.Select(p => p.Y).ToArray();
                double minX = allXs.Min();
                double maxX = allXs.Max();

                if (maxX > minX)
                {
                    double avgX = allXs.Average();
                    double avgY = allYs.Average();
                    double varX = allXs.Sum(x => (x - avgX) * (x - avgX));
                    if (varX > 0)
                    {
                        double a = allXs.Zip(allYs, (x, y) => (x - avgX) * (y - avgY)).Sum() / varX;
                        double b = avgY - a * avgX;

                        var trend = WpfPlotChart.Plot.Add.Line(minX, a * minX + b, maxX, a * maxX + b);
                        trend.LineColor = new ScottPlot.Color(120, 120, 120, 180);
                        trend.LineWidth = 1.5f;
                        trend.LinePattern = ScottPlot.LinePattern.Dashed;
                        trend.LegendText = "Linia trendu";
                    }
                }
            }

            WpfPlotChart.Plot.Axes.Bottom.Label.Text = labelX;
            WpfPlotChart.Plot.Axes.Left.Label.Text = "Średni koszt [zł]";
            WpfPlotChart.Plot.ShowLegend();

            odswiezWykres();
        }

        // ─────────────────────────────────────────────────────────────
        //  Histogramy
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Rysuje histogram wartości z podziałem na przedziały (bins) o podanej szerokości oraz podpisami osi.
        /// </summary>
        private void rysujHistogram(string tytul, double[] wartosci, double szerokosc, string labelX, string labelY)
        {
            if (!sprawdzDane(wartosci.Length)) return;

            resetujWykres(tytul);

            double min = wartosci.Min();
            double max = wartosci.Max();
            int liczbaBins = (int)Math.Ceiling((max - min) / szerokosc) + 1;

            var counts = new double[liczbaBins];
            foreach (var w in wartosci)
            {
                int bin = (int)((w - min) / szerokosc);
                if (bin >= liczbaBins) bin = liczbaBins - 1;
                counts[bin]++;
            }

            var bars = counts.Select((c, i) => new Bar
            {
                Position = min + i * szerokosc + szerokosc / 2,
                Value = c,
                FillColor = paleta(0),
                Size = szerokosc * 0.9
            }).ToArray();

            WpfPlotChart.Plot.Add.Bars(bars);
            WpfPlotChart.Plot.Axes.Margins(bottom: 0);
            WpfPlotChart.Plot.Axes.Bottom.Label.Text = labelX;
            WpfPlotChart.Plot.Axes.Left.Label.Text = labelY;

            odswiezWykres();
        }

        // ─────────────────────────────────────────────────────────────
        //  Dialogi wyboru podtypów
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Wyświetla prosty dialog wyboru grupowania dla zamówień wg marki/modelu/fabryki.
        /// </summary>
        private void pokazDialogZamowieniaMBF()
        {
            var wybor = pokazDialogWyboru("Zamówienia wg...", new[] { "Marka", "Model", "Fabryka" });
            if (wybor == null) return;

            IEnumerable<(string, double)> grupy = wybor switch
            {
                "Model" => dane.GroupBy(d => d.Model).Select(g => (g.Key, (double)g.Count())),
                "Fabryka" => dane.GroupBy(d => d.Fabryka.ToString()).Select(g => (g.Key, (double)g.Count())),
                _ => dane.GroupBy(d => d.Marka).Select(g => (g.Key, (double)g.Count()))
            };

            rysujSlupkowe($"Liczba zamówień wg {wybor}", grupy);
        }

        /// <summary>
        /// Wyświetla prosty dialog wyboru grupowania dla średniego kosztu wg marki/modelu/silnika.
        /// </summary>
        private void pokazDialogSredniKosztMBT()
        {
            var wybor = pokazDialogWyboru("Średni koszt wg...", new[] { "Marka", "Model", "Typ silnika" });
            if (wybor == null) return;

            IEnumerable<(string, double)> grupy = wybor switch
            {
                "Model" => dane.GroupBy(d => d.Model).Select(g => (g.Key, g.Average(x => (double)x.Koszt))),
                "Typ silnika" => dane.GroupBy(d => d.Typ_silnika.ToString()).Select(g => (g.Key, g.Average(x => (double)x.Koszt))),
                _ => dane.GroupBy(d => d.Marka).Select(g => (g.Key, g.Average(x => (double)x.Koszt)))
            };

            rysujSlupkowe($"Średni koszt wg {wybor}", grupy);
        }

        /// <summary>
        /// Wyświetla prosty dialog wyboru opcji z listy i zwraca wybraną wartość.
        /// </summary>
        private static string? pokazDialogWyboru(string tytul, string[] opcje)
        {
            var dialog = new Window
            {
                Title = tytul,
                Width = 260,
                Height = 160,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ResizeMode = ResizeMode.NoResize
            };

            var panel = new StackPanel { Margin = new Thickness(20) };
            var combo = new ComboBox { ItemsSource = opcje, SelectedIndex = 0, Margin = new Thickness(0, 0, 0, 12) };
            var btn = new Button { Content = "OK", Width = 80, HorizontalAlignment = System.Windows.HorizontalAlignment.Right };

            string? wynik = null;
            btn.Click += (_, _) => { wynik = combo.SelectedItem?.ToString(); dialog.Close(); };

            panel.Children.Add(combo);
            panel.Children.Add(btn);
            dialog.Content = panel;
            dialog.ShowDialog();

            return wynik;
        }

        // ─────────────────────────────────────────────────────────────
        //  Walidacja i paleta kolorów
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Sprawdza czy zestaw danych nie jest pusty; jeśli pusty — wyświetla komunikat.
        /// </summary>
        private bool sprawdzDane(int liczbaElementow)
        {
            if (liczbaElementow > 0) return true;
            MessageBox.Show("Brak danych do wyświetlenia dla aktualnych filtrów.", "Brak danych",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return false;
        }

        /// <summary>
        /// Zwraca kolor z predefiniowanej palety na podstawie indeksu.
        /// </summary>
        private static ScottPlot.Color paleta(int index)
        {
            ScottPlot.Color[] kolory =
            [
                new ScottPlot.Color(67, 133, 245),
                new ScottPlot.Color(251, 188, 4),
                new ScottPlot.Color(52, 168, 83),
                new ScottPlot.Color(234, 67, 53),
                new ScottPlot.Color(154, 102, 232),
                new ScottPlot.Color(255, 138, 101),
                new ScottPlot.Color(38, 198, 218),
                new ScottPlot.Color(129, 199, 132),
                new ScottPlot.Color(240, 98, 146),
                new ScottPlot.Color(255, 213, 79),
            ];
            return kolory[index % kolory.Length];
        }
    }
}
