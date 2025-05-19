using AutoMapper;
using SchoolService.Dtos.University;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;
using SchoolService.Services.Interfaces;
using System.Runtime.CompilerServices;

namespace SchoolService.Services
{
    public class UniversityService : IUniversityService
    {

        private readonly IUniversityRepository _universityRepository;

        private readonly IMapper _mapper;


        public UniversityService(IUniversityRepository universityRepository, IMapper mapper)
        {
            _universityRepository = universityRepository;
            _mapper = mapper;
        }

        public async Task CreateUniversity(CreateUniversityDto createUniversityDto)
        {
            University university = _mapper.Map<University>(createUniversityDto);

            await _universityRepository.CreateAsync(university);
        }

        public async Task DeleteUniversity(int id)
        {
            await _universityRepository.DeleteAsync(id);
        }

        public async Task<List<ResultUniversityDto>> GetAllUniversities()
        {
            var universities = await _universityRepository.GetListAsync();

            return _mapper.Map<List<ResultUniversityDto>>(universities);
        }

        public async Task<ResultUniversityDto> GetUniversityById(int id)
        {
            var university = await _universityRepository.GetByIdAsync(id);

            return _mapper.Map<ResultUniversityDto>(university);
        }

        public async Task UpdateUniversity(UpdateUniversityDto updateUniversityDto, int id)
        {
            var uni = await _universityRepository.GetByIdAsync(id);

            _mapper.Map(uni, updateUniversityDto);

            await _universityRepository.UpdateAsync(uni);
        }
    }
}
