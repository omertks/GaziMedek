using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolService.Dtos.Teacher;
using SchoolService.Services.Interfaces;

namespace SchoolService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeacherController : Controller
    {

        private readonly ITeacherService _teacherService;


        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }


        [HttpPost]
        public async Task<IActionResult> SaveTeacher([FromBody] CreateTeacherDto createTeacherDto) // Burası dto alacak
        {

            await _teacherService.SaveTeacher(createTeacherDto);

            return Created("",""); // buraya bak daha sonrasında
        }


    }
}
