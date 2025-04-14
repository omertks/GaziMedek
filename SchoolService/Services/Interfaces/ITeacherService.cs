using SchoolService.Dtos.Teacher;
using SchoolService.Models;

namespace SchoolService.Services.Interfaces
{
    public interface ITeacherService
    {
        public Task SaveTeacher(CreateTeacherDto createTeacherDto);

        public void UpdateTeacher(Teacher teacher, int id);

        public void DeleteTeacher(int id);

        public Task<List<Teacher>> GetTeachers();

        public Teacher GetTeacherById(int id);
    }
}
