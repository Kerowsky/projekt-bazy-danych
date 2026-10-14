using Microsoft.EntityFrameworkCore;
using projekt_bazy_danych.Common.Repositories.Interfaces;
using projekt_bazy_danych.Entities;

namespace projekt_bazy_danych.Common.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext context;

        private readonly DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T?>> GetAllRecords()
        {
            return await dbSet.ToListAsync();
        }
    }
}
