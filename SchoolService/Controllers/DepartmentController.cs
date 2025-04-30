using Microsoft.AspNetCore.Mvc;
using SchoolService.Dtos.Department;
using SchoolService.Services.Interfaces;

namespace SchoolService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        // Buraya Sadece Admin Erişicek

        private IDepartmentService _departmentService;


        public DepartmentController(IDepartmentService DepartmentService)
        {
            _departmentService = DepartmentService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllDepartmentsByUniversity([FromQuery] int universityId)
        {
            var departments = await _departmentService.GetAllDepartmentByUniversitiy(universityId);


            return departments != null ? Ok(departments) : NotFound("Kayıtlı Departman Yok!!!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var Department = await _departmentService.GetDepartmentById(id);

            return Department != null ? Ok(Department) : NotFound("Bu Id ye Sahip Departman Yok!!!");

        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDto createDepartmentDto)
        {
            await _departmentService.CreateDepartment(createDepartmentDto);

            return Created("", "");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentDto updateDepartmentDto, int id)
        {
            await _departmentService.UpdateDepartment(updateDepartmentDto, id);

            return Ok(new { message = "Departman Başarılı Bir Şekilde Oluşturuldu!!!" });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departmentService.DeleteDepartment(id);

            return Ok(new { message = "Departman Başarılı Bir Şekilde Silindi!!!" });
        }
    }
}
