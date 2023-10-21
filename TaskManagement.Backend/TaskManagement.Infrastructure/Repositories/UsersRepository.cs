using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            SeedData();
        }

        public async Task<UserEntity> GetUser(string userId)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        }

        private void SeedData()
        {
            //if the there are no users, seed the database with mock data
            if (_context.Users.Count() == 0)
            {
                _context.AddRange(MockData.GetMockUsers());
                _context.SaveChanges();
            }
        }
    }
}
