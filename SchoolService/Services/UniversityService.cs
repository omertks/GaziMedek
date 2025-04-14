using SchoolService.Dtos.University;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;
using SchoolService.Services.Interfaces;

namespace SchoolService.Services
{
    public class UniversityService : IUniversityService
    {

        private readonly IUniversityRepository _universityRepository;


        public UniversityService(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public async Task CreateUniversity(CreateUniversityDto createUniversityDto)
        {
            University university = new University()
            {
                UniverstyName = createUniversityDto.Name
            };

            await _universityRepository.CreateAsync(university);
        }

        public async Task DeleteUniversity(int id)
        {
            await _universityRepository.DeleteAsync(id);
        }

        public async Task<List<University>> GetAllUniversities()
        {
            return await _universityRepository.GetListAsync();
        }

        public async Task<University> GetUniversityById(int id)
        {
            return await _universityRepository.GetByIdAsync(id);
        }

        public async Task UpdateUniversity(UpdateUniversityDto updateUniversityDto, int id)
        {
            var uni = await _universityRepository.GetByIdAsync(id);

            uni.UniverstyName = updateUniversityDto.Name;

            await _universityRepository.UpdateAsync(uni);
        }
    }
}
