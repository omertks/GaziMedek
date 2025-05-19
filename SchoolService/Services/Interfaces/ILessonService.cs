using SchoolService.Dtos.Lesson;
using SchoolService.Models;

namespace SchoolService.Services.Interfaces
{
    public interface ILessonService
    {

        public Task<List<ResultLessonDto>> GetLessons();

        public Task<ResultLessonDto> GetLessonById(int id);

        public Task<List<ResultLessonDto>> GetLessonsByUserId(int id);
        
        public Task<List<ResultLessonDto>> GetLessonsByDepartmentId(int id);
        
        public Task<List<ResultLessonDto>> GetLessonsByUniversityId(int id);





        public Task CreateLesson(CreateLessonDto createLessonDto);

        public Task UpdateLesson(UpdateLessonDto updateLessonDto, int id);

        public Task DeleteLesson(int id);
    }
}
