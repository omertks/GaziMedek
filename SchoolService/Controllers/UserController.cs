using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolService.Dtos.User;
using SchoolService.Services;
using SchoolService.Services.Interfaces;

namespace SchoolService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }




        // tamamlandı
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            return user != null ? Ok(user) : NotFound();
        }

        // tamamlandı
        [HttpGet("userId/{id}")]
        public async Task<IActionResult> GetUserByUserId(int id)
        {
            var user = await _userService.GetUserByUserId(id);

            return user != null ? Ok(user) : NotFound();
        }

        // tamamlandı
        [HttpGet("teacher/department/{id}")]
        public async Task<IActionResult> GetTeachersByDepartmentId( int id)
        {
            var teachers = await _userService.GetTeachersByDepartmentId(id);

            return Ok(teachers);
        }



        // tamamlandı
        [HttpGet("manager/department/{id}")]
        public async Task<IActionResult> GetManagerByDepartmentId( int id)
        {
            var manager = await _userService.GetManagerByDepartmentId(id);

            return manager != null ? Ok(manager) : NotFound();
        }


        // Bu tamam
        [HttpPost]
        public async Task<IActionResult> SaveUser([FromBody] CreateUserDto createTeacherDto) 
        {
            await _userService.SaveUser(createTeacherDto);

            return Created("", ""); // buraya bak daha sonrasında
        }


        // tamamlandı
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);

            return NoContent();
        }


        // tamamlandı
        [HttpPut]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            await _userService.UpdateUser(updateUserDto, id);

            return Ok();
        }
    }
}
