using System.Linq.Expressions;

namespace SchoolService.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task<List<T>> GetListAsync();

        Task<T> GetByIdAsync(int id);

        Task<List<T>> GetFilteredListAsync(Expression<Func<T, bool>> expression);
    }
}
