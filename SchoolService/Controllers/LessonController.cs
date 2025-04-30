using Microsoft.AspNetCore.Mvc;
using SchoolService.Dtos.Lesson;
using SchoolService.Services.Interfaces;

namespace SchoolService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : Controller
    {
        private readonly ILessonService _LessonService;

        public LessonController(ILessonService LessonService)
        {
            _LessonService = LessonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLessonsByBaseLessonId([FromQuery] int baseLessonId, [FromQuery] int teacherId)
        {

            if(teacherId != null)
            {
                var lesson = await _LessonService.GetLessonsByTeacherId(teacherId);

                return lesson != null ? Ok(lesson) : NotFound("Bu Kullanıcının Dersi Yok");
            }

            var Lessons = await _LessonService.GetAllLessonsByBaseLessonId(baseLessonId);

            return Lessons != null ? Ok(Lessons) : NotFound("Lesson'ı Yok");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllLessons()
        {
            var Lessons = await _LessonService.GetAllLessons();

            return Lessons != null ? Ok(Lessons) : NotFound("Lesson Yok");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetLessonById(int id)
        {

            var Lesson = await _LessonService.GetLessonById(id);


            return Lesson != null ? Ok(Lesson) : NotFound("Bu Id ye sahip bir Lesson Yok");
        }




        [HttpPost]
        public async Task<IActionResult> CreateLesson(CreateLessonDto createLessonDto)
        {

            await _LessonService.CreateLesson(createLessonDto);

            return Created("", "");
        }


        [HttpPut]
        public async Task<IActionResult> UpdateLesson(UpdateLessonDto updateLessonDto, int id)
        {

            await _LessonService.UpdateLesson(updateLessonDto, id);

            return Ok(new { message = "Ders Güncellendi !!!!" });
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteLesson(int id)
        {

            await _LessonService.DeleteLesson(id);

            return Ok(new { message = "Ders Başarılı Bir Şekilde Silindi !!!" });
        }
    }
}
