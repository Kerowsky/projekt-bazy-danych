using projekt_bazy_danych.Entities;
using Microsoft.EntityFrameworkCore;

namespace projekt_bazy_danych.Common.Repositories
{
    public sealed class PojazdRepository : Repository<Pojazd>
    {
        private readonly DbSet<Pojazd> pojazdDbSet;

        public PojazdRepository(DbContext context) : base(context)
        {
            pojazdDbSet = base.context.Set<Pojazd>();
        }

        /// <summary>
        /// Zwraca pojazd bazujac na numerze vin
        /// </summary>
        /// <param name="vin_pojazdu"></param>
        /// <returns></returns>
        public async Task<Pojazd> GetPojazdByVin(int vin_pojazdu)
        {
            Pojazd pojazd = await pojazdDbSet.FirstAsync(p => p.Vin_pojazdu == vin_pojazdu);

            return pojazd;
        }
    }
}
