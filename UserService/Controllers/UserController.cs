using UserService.Dtos.User;
using UserService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserService.Service.Interfaces;
using System.Net.Http;
using UserService.Enums;


namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;




        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;

            //_jwtKey = configuration["Jwt:Key"];
            //_jwtIssuer = configuration["Jwt:Issuer"];
            //_jwtAudience = configuration["Jwt:Audience"];
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {

            var resultUserDto = _userService.LoginUser(userLoginDto.EmailAddress, userLoginDto.Password);



            if (resultUserDto.IsLogin == false)
                return Unauthorized(new { message = "Mail adresi veya Şifresi Hatalı" });


            return Ok(resultUserDto);

        }

        [HttpPost("save-user")]
        public async Task<IActionResult> SaveUser([FromBody] CreateUserDto createUserDto)
        {

            await _userService.SaveUserAsync(createUserDto);

            return Created("", new { Message = "User başarıyla her iki servise kaydedildi." });
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

            return Ok(new { Message = "Bu sayfa yetkili kişilerce girilir!!! "});
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAll();

            return Ok(users);
            
        }





    }
}
