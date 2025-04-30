using SchoolService.Db;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;

namespace SchoolService.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(SchoolServiceDbContext context) : base(context)
        {
        }
    }
}
