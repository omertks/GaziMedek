using SchoolService.Dtos.Lesson;
using SchoolService.Models;

namespace SchoolService.Services.Interfaces
{
    public interface ILessonService
    {

        public Task<List<Lesson>> GetAllLessonsByBaseLessonId(int id);

        public Task<List<Lesson>> GetAllLessons();

        public Task<Lesson> GetLessonById(int id);

        public Task<List<ResultLessonDto>> GetLessonsByTeacherId(int id);

        public Task CreateLesson(CreateLessonDto createLessonDto);

        public Task UpdateLesson(UpdateLessonDto updateLessonDto, int id);

        public Task DeleteLesson(int id);
    }
}
