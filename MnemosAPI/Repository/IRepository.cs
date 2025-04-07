namespace MnemosAPI.Repository
{
    public interface IRepository<T> where T : class 
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<T> AddAsync(T item);

        Task<T> UpdateAsync(T item);

        Task DeleteByIdAsync(int id);
    }
}
