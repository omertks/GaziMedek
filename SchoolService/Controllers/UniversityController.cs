using Microsoft.AspNetCore.Mvc;
using SchoolService.Dtos.University;
using SchoolService.Services.Interfaces;

namespace SchoolService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : Controller
    {
        // Buraya Sadece Admin Erişicek

        private IUniversityService _universityService;


        public UniversityController(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUniversities()
        {
            var universities = await _universityService.GetAllUniversities();


            return universities != null ? Ok(universities) : NotFound("Kayıtlı Üniversite Yok!!!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUniversityById(int id)
        {
            var university = await _universityService.GetUniversityById(id);

            return university != null ? Ok(university) : NotFound("Bu Id ye Sahip Üniversite Yok!!!");

        }

        [HttpPost]
        public async Task<IActionResult> CreateUniversity([FromBody] CreateUniversityDto createUniversityDto)
        {
            await _universityService.CreateUniversity(createUniversityDto);

            return Created("","");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUniversity([FromBody] UpdateUniversityDto updateUniversityDto, int id)
        {
            await _universityService.UpdateUniversity(updateUniversityDto, id);

            return Ok(new { message = "Üniversite Başarılı Bir Şekilde Oluşturuldu!!!"});
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUniversity(int id)
        {
            await _universityService.DeleteUniversity(id);

            return Ok(new {message = "Üniversite Başarılı Bir Şekilde Silindi!!!"});
        }
    }
}
