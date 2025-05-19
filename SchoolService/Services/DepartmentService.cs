using AutoMapper;
using SchoolService.Dtos.Department;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;
using SchoolService.Services.Interfaces;

namespace SchoolService.Services
{
    public class DepartmentService : IDepartmentService
    {

        private readonly IDepartmentRepository _departmentRepository;

        private readonly IMapper _mapper;


        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;

            _mapper = mapper;
        }

        // tamamlandı
        public async Task DeleteDepartment(int id)
        {
            await _departmentRepository.DeleteAsync(id);
        }

        public async Task<ResultDepartmentDto> GetDepartmentById(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            return _mapper.Map<ResultDepartmentDto>(department);
        }

        public async Task<List<ResultDepartmentDto>> GetDepartments()
        {
            var departments = await _departmentRepository.GetListAsync();

            return _mapper.Map<List<ResultDepartmentDto>>(departments);
        }

        public async Task<List<ResultDepartmentDto>> GetDepartmentsByUniversityId(int id)
        {
            var departments = await _departmentRepository.GetFilteredListAsync(d => d.UniversityId == id);

            return _mapper.Map<List<ResultDepartmentDto>>(departments);
        }




        public async Task SaveDepartment(CreateDepartmentDto createDepartmentDto)
        {
            var department = _mapper.Map<Department>(createDepartmentDto);

            await _departmentRepository.CreateAsync(department);
        }


        // Bunda Hata Çıkabilir
        public async Task UpdateDepartment(UpdateDepartmentDto updateDepartmentDto, int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            _mapper.Map(department, updateDepartmentDto);


            await _departmentRepository.UpdateAsync(department);
        }
    }
}
