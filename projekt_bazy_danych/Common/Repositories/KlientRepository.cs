using Microsoft.EntityFrameworkCore;
using projekt_bazy_danych.Common.Utils;
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

        public async Task<ResultWithMessage> AddKlient(Klient klient)
        {
            try
            {
                await klientDbSet.AddAsync(klient);
                await base.context.SaveChangesAsync();

                return new ResultWithMessage(true);
            }
            catch(Exception ex)
            {
                return new ResultWithMessage(false, "Blad dodawania nowego klienta do bazy danych");
            }
        }
    }
}
