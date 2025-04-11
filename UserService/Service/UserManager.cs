using Entity.Models;
using UserService.Repository.Interfaces;
using UserService.Service.Interfaces;

namespace UserService.Service
{
    public class UserManager : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Create(User entity)
        {
            await _userRepository.Create(entity);
        }

        public async Task Delete(int id)
        {
            await _userRepository.Delete(id);
        }

        public async Task<User> FindById(int id)
        {
            return await _userRepository.FindById(id);
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task Update(int id, User entity)
        {
            await _userRepository.Update(id, entity);
        }
    }
}
