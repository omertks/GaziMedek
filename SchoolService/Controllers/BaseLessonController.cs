using Microsoft.AspNetCore.Mvc;


using SchoolService.Dtos.BaseLesson;
using SchoolService.Services.Interfaces;
using System.Reflection.Metadata.Ecma335;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;

namespace SchoolService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BaseLessonController : Controller
    {
        private readonly IBaseLessonService _baseLessonService;

        public BaseLessonController(IBaseLessonService baseLessonService)
        {
            _baseLessonService = baseLessonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBaseLessonsByBranchId([FromQuery] int branchId)
        {
            var baseLessons = await _baseLessonService.GetAllBaseLessonsByBranchId(branchId);

            return baseLessons != null ? Ok(baseLessons) : NotFound("Base Lesson'ı Yok");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBaseLessons()
        {
            var baseLessons = await _baseLessonService.GetAllBaseLessons();

            return baseLessons != null ? Ok(baseLessons) : NotFound("Base Lesson Yok");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBaseLessonById(int id)
        {

            var baseLesson = await _baseLessonService.GetBaseLessonById(id);


            return baseLesson != null ? Ok(baseLesson) : NotFound("Bu Id ye sahip bir BaseLesson Yok");
        }


        [HttpPost]
        public async Task<IActionResult> CreateBaseLesson(CreateBaseLessonDto createBaseLessonDto)
        {

            await _baseLessonService.CreateBaseLesson(createBaseLessonDto);

            return Created("", "");
        }


        [HttpPut]
        public async Task<IActionResult> UpdateBaseLesson(UpdateBaseLessonDto updateBaseLessonDto, int id)
        {

            await _baseLessonService.UpdateBaseLesson(updateBaseLessonDto, id);

            return Ok(new { message = "Ders Güncellendi !!!!" });
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteBaseLesson(int id)
        {

            await _baseLessonService.DeleteBaseLesson(id);

            return Ok(new { message = "Ders Başarılı Bir Şekilde Silindi !!!" });
        }
    }
}
