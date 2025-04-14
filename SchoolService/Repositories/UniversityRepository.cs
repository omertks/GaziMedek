using SchoolService.Db;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;

namespace SchoolService.Repositories
{
    public class UniversityRepository : GenericRepository<University>, IUniversityRepository
    {
        public UniversityRepository(SchoolServiceDbContext context) : base(context)
        {
        }
    }
}
