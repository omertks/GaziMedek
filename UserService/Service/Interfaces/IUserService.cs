using UserService.Models;
using UserService.Dtos.User;

namespace UserService.Service.Interfaces
{
    public interface IUserService: IService<User>
    {

        public ResultUserDto LoginUser(string email, string password);

        public Task SaveUserAsync(CreateUserDto createUserDto);

    }
}
