using Microsoft.EntityFrameworkCore;
using projekt_bazy_danych.Common.Interfaces;

namespace projekt_bazy_danych.Common
{
    public sealed class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext context;

        private readonly DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T?>> GetValuesAsync()
        {
            return await dbSet.ToListAsync();
        }

    }
}
