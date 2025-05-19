using SchoolService.Db;
using SchoolService.Models;
using SchoolService.Repositories.Interfaces;

namespace SchoolService.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SchoolServiceDbContext context) : base(context)
        {
        }
    }
}
