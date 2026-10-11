using Microsoft.EntityFrameworkCore;
using projekt_bazy_danych.Entities;

namespace projekt_bazy_danych.Common.Repositories
{
    public sealed class KlientRepository : Repository<Klient>
    {
        private readonly DbSet<Klient> klientDbSet;

        public KlientRepository(DbContext context) : base(context)
        {
            klientDbSet = base.context.Set<Klient>();
        }

        public async Task<IEnumerable<Klient>> GetAllKlient()
        {
            return await klientDbSet.ToListAsync();
        }

        public async Task<Klient> GetKlientById(Guid id)
        {
            var test1 = id;
            var test2 = klientDbSet.First(e => e.Id == id);
            var test = await klientDbSet.FirstOrDefaultAsync(klient => klient.Id == id);
            return test;
        }
    }
}
