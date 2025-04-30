using SchoolService.Dtos.Department;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;
using SchoolService.Services.Interfaces;

namespace SchoolService.Services
{
    public class DepartmentService : IDepartmentService
    {

        private readonly IDepartmentRepository _departmentRepository;


        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }



        public async Task CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {

            Department department = new Department()
            {
                DepartmentName = createDepartmentDto.DepartmentName,
                UniversityId = createDepartmentDto.UniversityId
            };

            await _departmentRepository.CreateAsync(department);
        }

        public async Task DeleteDepartment(int id)
        {
            await _departmentRepository.DeleteAsync(id);
        }

        public async Task<List<Department>> GetAllDepartmentByUniversitiy(int id)
        {
            var departments = await _departmentRepository.GetFilteredListAsync(d => d.UniversityId == id);

            return departments;
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await _departmentRepository.GetByIdAsync(id);
        }

        public async Task UpdateDepartment(UpdateDepartmentDto updateDepartmentDto, int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            department.DepartmentName = updateDepartmentDto.DepartmentName;

            await _departmentRepository.UpdateAsync(department);
        }
    }
}
