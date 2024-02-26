using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser()
        {
            var userWithMaxOrders = await _context.Users
                .Include(o => o.Orders)
                .OrderByDescending(u => u.Orders.Count)
                .FirstOrDefaultAsync();

            return userWithMaxOrders;
        }

        public Task<List<User>> GetUsers()
        {
            return _context.Users.Where(s => s.Status == Enums.UserStatus.Inactive).ToListAsync();
        }
    }
}
