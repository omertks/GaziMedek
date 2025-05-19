using Microsoft.AspNetCore.Mvc;
using SchoolService.Dtos.Lesson;
using SchoolService.Services.Interfaces;

namespace SchoolService.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        // GET: api/lesson
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lessons = await _lessonService.GetLessons();
            return Ok(lessons);
        }

        // GET: api/lessons/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lesson = await _lessonService.GetLessonById(id);

            return lesson != null ? Ok(lesson) : NotFound();
        }

        // GET: api/lessons/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var lessons = await _lessonService.GetLessonsByUserId(userId);
            return Ok(lessons);
        }

        // GET: api/lessons/department/5
        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetByDepartmentId(int departmentId)
        {
            var lessons = await _lessonService.GetLessonsByDepartmentId(departmentId);
            return Ok(lessons);
        }

        // GET: api/lessons/university/5
        [HttpGet("university/{universityId}")]
        public async Task<IActionResult> GetByUniversityId(int universityId)
        {
            var lessons = await _lessonService.GetLessonsByUniversityId(universityId);
            return Ok(lessons);
        }

        // POST: api/lessons
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLessonDto dto)
        {
            await _lessonService.CreateLesson(dto);
            return Ok(new { message = "Ders Oluşturudu." });
        }

        // PUT: api/lessons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLessonDto dto)
        {
            await _lessonService.UpdateLesson(dto, id);
            return Ok(new { message = "Ders Güncellendi." });
        }

        // DELETE: api/lessons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _lessonService.DeleteLesson(id);
            return Ok(new { message = "Ders Silindi." });
        }
    }

}
