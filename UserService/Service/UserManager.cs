using Dto.Dtos.User;
using Entity.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Dtos.User;
using UserService.Repository.Interfaces;
using UserService.Service.Interfaces;

namespace UserService.Service
{
    public class UserManager : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        // Jwt için gerekli parametreler
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;


        public UserManager(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;

            _jwtKey = configuration["Jwt:Key"];
            _jwtIssuer = configuration["Jwt:Issuer"];
            _jwtAudience = configuration["Jwt:Audience"];
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

        public ResultUserDto LoginUser(string email, string password)
        {

            var user = _userRepository.GetUserByEmailAndPassword(email, password);

            ResultUserDto resultUserDto = new ResultUserDto();
        
            if(user == null)
            {
                resultUserDto.IsLogin = false;
                return resultUserDto;
            }

            else
            {
                var token = GenerateJwtToken(user);

                resultUserDto.IsLogin = true;
                resultUserDto.Token = token;
                resultUserDto.UserId = user.Id;
                resultUserDto.TeacherId = user.TeacherId;

                resultUserDto.Role = user.Role.ToString();

                return resultUserDto;
            }

        }



        public async Task Update(int id, User entity)
        {
            await _userRepository.Update(id, entity);
        }


        private string GenerateJwtToken(User user)
        {
            // Tokenın içerisine gömmek istediğim verileri burada ekliyorum
            var claims = new[]
 {
    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Kullanıcı ID'si
    new Claim(ClaimTypes.Name, user.FirstName),              // Kullanıcı Adı
    new Claim(ClaimTypes.Email, user.Email),                 // Kullanıcı Email
    new Claim(JwtRegisteredClaimNames.Sub, user.Email),      // Konvansiyonel 'sub' (Subject) claim, burada email kullanılıyor. Buraya Bak
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // JWT'nin benzersiz kimliği (JTI)
};

            // Şifreyi şifreleme
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token Oluşturma
            var token = new JwtSecurityToken(
                _jwtIssuer,
                _jwtAudience,
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
