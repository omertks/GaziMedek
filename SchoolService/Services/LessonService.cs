using AutoMapper;
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

        private readonly IMapper _mapper;

        public LessonService(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;

            _mapper = mapper;
        }

        public async Task CreateLesson(CreateLessonDto createLessonDto)
        {
            var lesson = _mapper.Map<Lesson>(createLessonDto);

            await _lessonRepository.CreateAsync(lesson);
        }

        public async Task DeleteLesson(int id)
        {
            await _lessonRepository.DeleteAsync(id);
        }

        public async Task<ResultLessonDto> GetLessonById(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);

            return _mapper.Map<ResultLessonDto>(lesson);
        }

        public async Task<List<ResultLessonDto>> GetLessons()
        {
            var lessons = await _lessonRepository.GetListAsync();

            return _mapper.Map<List<ResultLessonDto>>(lessons);
        }

        public async Task<List<ResultLessonDto>> GetLessonsByDepartmentId(int id)
        {
            var lessons = await _lessonRepository.GetFilteredListAsync(l => l.DepartmentId == id);

            return _mapper.Map<List<ResultLessonDto>>(lessons);
        }

        // Bu çalışmaya bilir buna dikkat et
        public async Task<List<ResultLessonDto>> GetLessonsByUniversityId(int id)
        {
            var lessons = await _lessonRepository.GetFilteredListAsync(l => l.Department.UniversityId == id);

            return _mapper.Map<List<ResultLessonDto>>(lessons);
        }


        public async Task<List<ResultLessonDto>> GetLessonsByUserId(int id)
        {
            var lessons = await _lessonRepository.GetFilteredListAsync(l => l.TeacherId == id);

            return _mapper.Map<List<ResultLessonDto>>(lessons);
        }

        public async Task UpdateLesson(UpdateLessonDto updateLessonDto, int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);

            _mapper.Map(lesson, updateLessonDto);

            await _lessonRepository.UpdateAsync(lesson);
        }
    }
}
