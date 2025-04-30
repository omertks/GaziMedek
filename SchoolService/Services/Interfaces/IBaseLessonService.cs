using SchoolService.Dtos.BaseLesson;
using SchoolService.Dtos.Branch;
using SchoolService.Models;

namespace SchoolService.Services.Interfaces
{
    public interface IBaseLessonService
    {

        public Task<List<BaseLesson>> GetAllBaseLessonsByBranchId(int id);

        public Task<List<BaseLesson>> GetAllBaseLessons();

        public Task<BaseLesson> GetBaseLessonById(int id);

        public Task CreateBaseLesson(CreateBaseLessonDto createBaseLessonDto);

        public Task UpdateBaseLesson(UpdateBaseLessonDto updateBaseLessonDto, int id);

        public Task DeleteBaseLesson(int id);


    }
}
