namespace projekt_bazy_danych.Common.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T?>> GetValuesAsync();
    }
}
