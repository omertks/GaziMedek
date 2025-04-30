using SchoolService.Dtos.Lesson;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;
using SchoolService.Services.Interfaces;
using System.Collections;

namespace SchoolService.Services
{
    public class LessonService : ILessonService
    {

        private readonly ILessonRepository _lessonRepository;

        private readonly IBaseLessonRepository _baseLessonRepository;

        public LessonService(ILessonRepository lessonRepository, IBaseLessonRepository baseLessonRepository)
        {
            _lessonRepository = lessonRepository;
            _baseLessonRepository = baseLessonRepository;
        }



        public async Task CreateLesson(CreateLessonDto createLessonDto)
        {
            Lesson lesson = new Lesson()
            {
                AcademicYear = createLessonDto.AcademicYear,
                BaseLessonId = createLessonDto.BaseLessonId,
                TeacherId = createLessonDto.TeacherId
            };

            await _lessonRepository.CreateAsync(lesson);
        }

        public async Task DeleteLesson(int id)
        {
            await _lessonRepository.DeleteAsync(id);
        }

        public async Task<List<Lesson>> GetAllLessons()
        {
            return await _lessonRepository.GetListAsync();
        }

        public async Task<List<Lesson>> GetAllLessonsByBaseLessonId(int id)
        {
            return await _lessonRepository.GetFilteredListAsync(l => l.BaseLessonId == id);
        }

        public async Task<List<ResultLessonDto>> GetLessonsByTeacherId(int id)
        {
            var lessons = await _lessonRepository.GetFilteredListAsync(l => l.TeacherId == id);

            List<ResultLessonDto> result = new List<ResultLessonDto>();

            foreach (var lesson in lessons)
            {
                var baseLesson = await _baseLessonRepository.GetByIdAsync(lesson.BaseLessonId);

                var rs = new ResultLessonDto()
                {
                    Id = lesson.Id,
                    LessonName = baseLesson.Name,
                    LessonCode = baseLesson.Code
                };

                result.Add(rs);
            }

            return result;

        }

        public async Task<Lesson> GetLessonById(int id)
        {
            return await _lessonRepository.GetByIdAsync(id);
        }



        public async Task UpdateLesson(UpdateLessonDto updateLessonDto, int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);

            lesson.AcademicYear = updateLessonDto.AcademicYear;
            lesson.TeacherId = updateLessonDto.TeacherId;
            lesson.BaseLessonId = updateLessonDto.BaseLessonId;

            await _lessonRepository.UpdateAsync(lesson);
        }
    }
}
