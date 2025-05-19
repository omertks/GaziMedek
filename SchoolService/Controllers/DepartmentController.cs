using Microsoft.AspNetCore.Mvc;
using SchoolService.Dtos.Department;
using SchoolService.Services.Interfaces;

namespace SchoolService.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/department
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetDepartments();
            return Ok(departments);
        }

        // GET: api/departments/university/5
        [HttpGet("university/{universityId}")]
        public async Task<IActionResult> GetByUniversityId(int universityId)
        {
            var departments = await _departmentService.GetDepartmentsByUniversityId(universityId);
            return Ok(departments);
        }

        // GET: api/departments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _departmentService.GetDepartmentById(id);
            
            return department != null ? Ok(department) : NotFound();
        }

        // POST: api/department
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDto dto)
        {
            await _departmentService.SaveDepartment(dto);
            return Ok(new { message = "Department created successfully." });
        }

        // PUT: api/departments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentDto dto)
        {
            await _departmentService.UpdateDepartment(dto, id);
            return Ok(new { message = id + "'li Departman Güncellendi." });
        }

        // DELETE: api/departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _departmentService.DeleteDepartment(id);
            return Ok(new { message = id + "'li Departman Silindi." });
        }
    }

}
