namespace UserService.Service.Interfaces
{
    public interface IService<T> where T : class
    {
        public Task<List<T>> GetAll();

        public Task<T> FindById(int id);

        public Task Create(T entity);

        public Task Update(int id, T entity);

        public Task Delete(int id);
    }
}
