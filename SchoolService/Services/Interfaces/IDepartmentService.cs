using SchoolService.Dtos.Department;
using SchoolService.Dtos.University;
using SchoolService.Models;

namespace SchoolService.Services.Interfaces
{
    public interface IDepartmentService
    {
        public Task<List<Department>> GetAllDepartmentByUniversitiy(int id);

        public Task<Department> GetDepartmentById(int id);

        public Task CreateDepartment(CreateDepartmentDto createDepartmentDto);

        public Task UpdateDepartment(UpdateDepartmentDto updateDepartmentDto, int id);

        public Task DeleteDepartment(int id);
    }
}
