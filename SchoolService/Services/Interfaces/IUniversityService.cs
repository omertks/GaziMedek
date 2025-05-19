using Microsoft.AspNetCore.Mvc;
using SchoolService.Dtos.University;
using SchoolService.Models;

namespace SchoolService.Services.Interfaces
{
    public interface IUniversityService
    {

        public Task<List<ResultUniversityDto>> GetAllUniversities();

        public Task<ResultUniversityDto> GetUniversityById(int id);


        public Task CreateUniversity(CreateUniversityDto createUniversityDto);

        public Task UpdateUniversity(UpdateUniversityDto updateUniversityDto, int id);

        public Task DeleteUniversity(int id);

    }
}
