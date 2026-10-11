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
    }
}
