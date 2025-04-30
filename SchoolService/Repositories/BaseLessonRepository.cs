using SchoolService.Db;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;

namespace SchoolService.Repositories
{
    public class BaseLessonRepository : GenericRepository<BaseLesson>, IBaseLessonRepository
    {
        public BaseLessonRepository(SchoolServiceDbContext context) : base(context)
        {
        }
    }
}
