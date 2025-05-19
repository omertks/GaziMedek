using UserService.Models;

namespace UserService.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<List<T>> GetAll();

        public Task<T> FindById(int id);

        public Task<int> Create(T entity);

        public Task Update(int id, T entity);

        public Task Delete(int id);
    }
}
