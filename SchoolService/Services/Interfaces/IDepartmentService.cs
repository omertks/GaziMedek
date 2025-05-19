using Microsoft.AspNetCore.Mvc.Filters;
using SchoolService.Dtos.Department;
using SchoolService.Dtos.University;
using SchoolService.Models;

namespace SchoolService.Services.Interfaces
{
    public interface IDepartmentService
    {

        public Task<List<ResultDepartmentDto>> GetDepartments();
        
        public Task<List<ResultDepartmentDto>> GetDepartmentsByUniversityId(int id);

        public Task<ResultDepartmentDto> GetDepartmentById(int id);

        


        public Task SaveDepartment(CreateDepartmentDto createDepartmentDto);

        public Task UpdateDepartment(UpdateDepartmentDto updateDepartmentDto, int id);

        public Task DeleteDepartment(int id);
    }
}
