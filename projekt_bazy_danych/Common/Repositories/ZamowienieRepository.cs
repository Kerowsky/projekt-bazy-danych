using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Zamowienie>> GetAllRecords()
        {
            List<Zamowienie> zamowienia = await zamowieniaDbSet.ToListAsync();

            return zamowienia;
        }
    }
}
