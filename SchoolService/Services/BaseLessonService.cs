using SchoolService.Dtos.BaseLesson;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;
using SchoolService.Services.Interfaces;

namespace SchoolService.Services
{
    public class BaseLessonService : IBaseLessonService
    {

        private readonly IBaseLessonRepository _baseLessonRepository;


        public BaseLessonService(IBaseLessonRepository baseLessonRepository)
        {
            _baseLessonRepository = baseLessonRepository;
        }


        public async Task CreateBaseLesson(CreateBaseLessonDto createBaseLessonDto)
        {
            var baseLesson = new BaseLesson()
            {
                Name = createBaseLessonDto.Name,
                Code = createBaseLessonDto.Code,
                TotalAkts = createBaseLessonDto.TotalAkts,
                BranchId = createBaseLessonDto.BranchId
            };

            await _baseLessonRepository.CreateAsync(baseLesson);
        }

        public async Task DeleteBaseLesson(int id)
        {
            await _baseLessonRepository.DeleteAsync(id);
        }

        public async Task<List<BaseLesson>> GetAllBaseLessons()
        {
            return await _baseLessonRepository.GetListAsync();
        }

        public async Task<List<BaseLesson>> GetAllBaseLessonsByBranchId(int id)
        {
            return await _baseLessonRepository.GetFilteredListAsync(bl => bl.BranchId == id);
        }

        public async Task<BaseLesson> GetBaseLessonById(int id)
        {
            return await _baseLessonRepository.GetByIdAsync(id);
        }

        public async Task UpdateBaseLesson(UpdateBaseLessonDto updateBaseLessonDto, int id)
        {
            var baseLesson = await _baseLessonRepository.GetByIdAsync(id);

            baseLesson.Name = updateBaseLessonDto.Name;
            baseLesson.Code = updateBaseLessonDto.Code;
            baseLesson.TotalAkts = updateBaseLessonDto.TotalAkts;
            baseLesson.BranchId = updateBaseLessonDto.BranchId;


            await _baseLessonRepository.UpdateAsync(baseLesson);
        }
    }
}
