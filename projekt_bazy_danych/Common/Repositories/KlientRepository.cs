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

        public async Task<Klient> GetKlientById(Guid id)
        {
            Klient klient = await klientDbSet.FirstOrDefaultAsync(klient => klient.Id == id);

            return klient;
        }

        public async Task AddKlient(Klient klient)
        {
            try
            {
                await klientDbSet.AddAsync(klient);
            }
            catch(Exception ex)
            {
                throw new Exception("Blad dodawania nowego klienta: " + ex.Message);
            }
        }
    }
}
