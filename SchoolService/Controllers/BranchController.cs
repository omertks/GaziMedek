using Microsoft.AspNetCore.Mvc;
using SchoolService.Dtos.Branch;
using SchoolService.Services.Interfaces;

namespace SchoolService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : Controller
    {
        // Buraya Sadece Admin Erişicek

        private IBranchService _branchService;


        public BranchController(IBranchService BranchService)
        {
            _branchService = BranchService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllBranchsByDepartment([FromQuery] int DepartmentId)
        {
            var Branchs = await _branchService.GetAllBranchByDepartment(DepartmentId);


            return Branchs != null ? Ok(Branchs) : NotFound("Kayıtlı Dal Yok!!!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranchById(int id)
        {
            var Branch = await _branchService.GetBranchById(id);

            return Branch != null ? Ok(Branch) : NotFound("Bu Id ye Sahip Dal Yok!!!");

        }

        [HttpPost]
        public async Task<IActionResult> CreateBranch([FromBody] CreateBranchDto createBranchDto)
        {
            await _branchService.CreateBranch(createBranchDto);

            return Created("", "");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch([FromBody] UpdateBranchDto updateBranchDto, int id)
        {
            await _branchService.UpdateBranch(updateBranchDto, id);

            return Ok(new { message = "Dal Başarılı Bir Şekilde Oluşturuldu!!!" });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            await _branchService.DeleteBranch(id);

            return Ok(new { message = "Dal Başarılı Bir Şekilde Silindi!!!" });
        }
    }
}
