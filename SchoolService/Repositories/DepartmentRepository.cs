using SchoolService.Db;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;

namespace SchoolService.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(SchoolServiceDbContext context) : base(context)
        {
        }
    }
}
