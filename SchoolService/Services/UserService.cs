
using AutoMapper;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolService.Dtos.User;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;
using SchoolService.Services.Interfaces;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SchoolService.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;

            _mapper = mapper;
        }

        // Tamam
        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteAsync(id);
        }


        // tamamlandı
        public async Task<ResultUserDto> GetManagerByDepartmentId(int id)
        {
            var manager = (await _userRepository.GetFilteredListAsync(u => u.DepartmentId == id && u.Role == Enums.Role.MANAGER)).FirstOrDefault();
            
            if (manager == null)
                throw new Exception("User not found");


            return _mapper.Map<ResultUserDto>(manager);
        }


        // tammalandı
        public async Task<ResultUserDto> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return _mapper.Map<ResultUserDto>(user);
        }


        // tamamlandı
        public async Task<ResultUserDto> GetUserByUserId(int id)
        {
            var user = (await _userRepository.GetFilteredListAsync(u => u.UserId == id))[0];

            return _mapper.Map<ResultUserDto>(user);
        }


        // tamamlandı
        public async Task<List<ResultUserDto>> GetUsers()
        {
            var users = await _userRepository.GetListAsync();

            return _mapper.Map<List<ResultUserDto>>(users);
        }

        // tamamlandı
        public async Task<List<ResultUserDto>> GetTeachersByDepartmentId(int id)
        {
            var teachers = (await _userRepository.GetFilteredListAsync(u => u.DepartmentId == id && u.Role == Enums.Role.TEACHER));

            return _mapper.Map<List<ResultUserDto>>(teachers);
        }


        // tamamlandı
        public async Task SaveUser(CreateUserDto createUserDto)
        {
            User user = _mapper.Map<User>(createUserDto);

            await _userRepository.CreateAsync(user);
        }


        // tammalandı
        public async Task UpdateUser(UpdateUserDto updateUserDto, int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new Exception("User not found");

            _mapper.Map(updateUserDto, user);

            await _userRepository.UpdateAsync(user);
        }


    }
}
