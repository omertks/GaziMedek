using SchoolService.Db;
using SchoolService.Dtos.Teacher;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;

namespace SchoolService.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(SchoolServiceDbContext context) : base(context)
        {
        }
    }
}
