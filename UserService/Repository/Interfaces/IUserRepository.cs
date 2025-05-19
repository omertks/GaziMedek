using UserService.Models;

namespace UserService.Repository.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {

        public User GetUserByEmailAndPassword(string email, string password);
    }
}
