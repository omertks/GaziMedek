
using SchoolService.Dtos.User;
using SchoolService.Models;

namespace SchoolService.Services.Interfaces
{
    public interface IUserService
    {
        public Task SaveUser(CreateUserDto createUserDto);

        public Task UpdateUser(UpdateUserDto updateUserDto, int id);

        public Task DeleteUser(int id);

        public Task<List<ResultUserDto>> GetUsers();

        public Task<ResultUserDto> GetUserById(int id);

        public Task<ResultUserDto> GetUserByUserId(int id);

        public Task<List<ResultUserDto>> GetTeachersByDepartmentId(int id);


        public Task<ResultUserDto> GetManagerByDepartmentId(int id);
    }
}
