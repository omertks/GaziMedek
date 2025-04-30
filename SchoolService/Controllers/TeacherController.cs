using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolService.Dtos.Teacher;
using SchoolService.Services.Interfaces;

namespace SchoolService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : Controller
    {

        private readonly ITeacherService _teacherService;


        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTeacherByUserId([FromQuery] int userId)
        {
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTeachers([FromQuery] int? branchId)
        {
            return Ok();
        }




        [HttpPost]
        public async Task<IActionResult> SaveTeacher([FromBody] CreateTeacherDto createTeacherDto) // Burası dto alacak
        {

            await _teacherService.SaveTeacher(createTeacherDto);

            return Created("", ""); // buraya bak daha sonrasında
        }


    }
}
