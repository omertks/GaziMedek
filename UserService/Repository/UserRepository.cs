using Entity.Models;
using Microsoft.EntityFrameworkCore;
using UserService.Db;
using UserService.Repository.Interfaces;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly UserServiceDbContext _context;

        public UserRepository(UserServiceDbContext context)
        {
            _context = context;
        }


        public async Task Create(User user)
        {
            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            User user = await FindById(id);

            if (user != null)
            {
                _context.users.Remove(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Buraya Bak
                throw new Exception();
            }
        }

        public async Task<User> FindById(int id)
        {
            // Burası null Gelebilir bunu çöz
            return await _context.users.FindAsync(id);
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _context.users.ToListAsync();

            return users;
        }

        public async Task Update(int id, User user)
        {
            User dbUser = await FindById(id);

            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Email = user.Email;
            dbUser.Password = user.Password;

            await _context.SaveChangesAsync();

        }
    }
}
