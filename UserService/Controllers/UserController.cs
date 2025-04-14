using Dto.Dtos.User;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserService.Service.Interfaces;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        // Jwt için gerekli parametreler
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;


        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;

            _jwtKey = configuration["Jwt:Key"];
            _jwtIssuer = configuration["Jwt:Issuer"];
            _jwtAudience = configuration["Jwt:Audience"];
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var users = await _userService.GetAll();

            // Burası daha verimli hale getirilebilinir. Tüm kullanıcılar çekilmedende yapılabilir.
            var user = users.FirstOrDefault(u => u.Email == userLoginDto.EmailAddress && u.Password == userLoginDto.Password);


            if (user == null)
                return Unauthorized(new { message = "Mail adresi veya Şifresi Hatalı" });

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });

        }

        [HttpPost("regiter")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            User user = new User();

            user.FirstName = userRegisterDto.FirstName;
            user.LastName = userRegisterDto.LastName;
            user.Email = userRegisterDto.Email;
            user.Password = userRegisterDto.Password;

            await _userService.Create(user);

            return Ok(new { Message = "Kullanıcı Başarılı Bir Şekilde Oluşturuldu" });
        }


        [Authorize]
        [HttpGet("yetkili")]
        public async Task<IActionResult> AuthorizeMethod()
        {

            var users = await _userService.GetAll();

            return Ok(new { Message = "Bu sayfa yetkili kişilerce girilir!!! ", Users = users });



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
