using Microsoft.EntityFrameworkCore;
using projekt_bazy_danych.Common.Utils;
using projekt_bazy_danych.Entities;

namespace projekt_bazy_danych.Common.Repositories
{
    public sealed class ZamowienieRepository : Repository<Zamowienie>
    {
        private readonly DbSet<Zamowienie> zamowieniaDbSet;

        public ZamowienieRepository(DbContext context) : base(context)
        {
            zamowieniaDbSet = base.context.Set<Zamowienie>();
        }

        public async Task<Zamowienie> GetZamowienieByKlientId(Guid klientId)
        {
            Zamowienie zamowienie = await zamowieniaDbSet.FirstAsync(e => e.KlientId == klientId);

            return zamowienie;
        }

        /// <summary>
        /// Pobiera zamówienia wraz z powiązanymi encjami Klient i Pojazd, filtrując je po zadanych kryteriach.
        /// </summary>
        /// <param name="filtr">Obiekt kryteriów filtrowania.</param>
        /// <returns>Lista zamówień spełniających warunki.</returns>
        public async Task<List<Zamowienie>> GetZamowieniaFiltered(Domain.DTOs.ZamowienieFiltrDTO? filtr = null)
        {
            IQueryable<Zamowienie> query = zamowieniaDbSet
                .AsNoTracking()
                .Include(z => z.Klient)
                .Include(z => z.Pojazd);

            if (filtr == null)
            {
                return await query.ToListAsync();
            }

            if (!string.IsNullOrWhiteSpace(filtr.Imie))
            {
                query = query.Where(z => z.Klient != null && z.Klient.Imie.Contains(filtr.Imie));
            }

            if (!string.IsNullOrWhiteSpace(filtr.Nazwisko))
            {
                query = query.Where(z => z.Klient != null && z.Klient.Nazwisko.Contains(filtr.Nazwisko));
            }

            if (filtr.Vin.HasValue)
            {
                query = query.Where(z => z.Vin_pojazdu == filtr.Vin.Value);
            }

            if (!string.IsNullOrWhiteSpace(filtr.Marka))
            {
                query = query.Where(z => z.Pojazd != null && z.Pojazd.Model.StartsWith(filtr.Marka));
            }

            if (!string.IsNullOrWhiteSpace(filtr.Model))
            {
                query = query.Where(z => z.Pojazd != null && z.Pojazd.Model.Contains(filtr.Model));
            }

            if (filtr.TypSilnika.HasValue)
            {
                query = query.Where(z => z.Pojazd != null && z.Pojazd.Typ_silnika == filtr.TypSilnika.Value);
            }

            if (filtr.Fabryka.HasValue)
            {
                query = query.Where(z => z.Pojazd != null && z.Pojazd.Fabryka == filtr.Fabryka.Value);
            }

            if (filtr.KosztOd.HasValue)
            {
                query = query.Where(z => z.Koszt >= filtr.KosztOd.Value);
            }

            if (filtr.KosztDo.HasValue)
            {
                query = query.Where(z => z.Koszt <= filtr.KosztDo.Value);
            }

            if (filtr.MocOd.HasValue)
            {
                query = query.Where(z => z.Pojazd != null && z.Pojazd.Moc >= filtr.MocOd.Value);
            }

            if (filtr.MocDo.HasValue)
            {
                query = query.Where(z => z.Pojazd != null && z.Pojazd.Moc <= filtr.MocDo.Value);
            }

            if (filtr.PojemnoscOd.HasValue)
            {
                query = query.Where(z => z.Pojazd != null && z.Pojazd.Pojemnosc >= filtr.PojemnoscOd.Value);
            }

            if (filtr.PojemnoscDo.HasValue)
            {
                query = query.Where(z => z.Pojazd != null && z.Pojazd.Pojemnosc <= filtr.PojemnoscDo.Value);
            }

            if (filtr.DataOd.HasValue)
            {
                DateTime poczatekDnia = filtr.DataOd.Value.Date;
                query = query.Where(z => z.Data >= poczatekDnia);
            }

            if (filtr.DataDo.HasValue)
            {
                DateTime koniecDnia = filtr.DataDo.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(z => z.Data <= koniecDnia);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Usuwa zamówienie o podanym identyfikatorze z bazy danych.
        /// </summary>
        /// <param name="idZamowienia">Identyfikator usuwanego zamówienia.</param>
        /// <returns>Obiekt ResultWithMessage informujący o powodzeniu lub błędzie operacji.</returns>
        public async Task<ResultWithMessage> UsunZamowienie(Guid idZamowienia)
        {
            try
            {
                Zamowienie? zamowienie = await zamowieniaDbSet.FirstOrDefaultAsync(z => z.Id_zamowienia == idZamowienia);
                if (zamowienie == null)
                {
                    return new ResultWithMessage(false, "Nie odnaleziono zamówienia w bazie danych.");
                }

                zamowieniaDbSet.Remove(zamowienie);
                await base.context.SaveChangesAsync();

                return new ResultWithMessage(true, "Pomyślnie usunięto zamówienie.");
            }
            catch (Exception ex)
            {
                return new ResultWithMessage(false, $"Błąd podczas usuwania zamówienia z bazy danych: {ex.Message}");
            }
        }
    }
}
