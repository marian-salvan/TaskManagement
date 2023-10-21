using Microsoft.EntityFrameworkCore;
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

        //crud methods for UserEntity
        public async Task<bool> CreateUser(UserEntity userEntity)
        {
            _context.Add(userEntity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUser(UserEntity userEntity)
        {
            _context.Update(userEntity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUser(UserEntity userEntity)
        {
            _context.Remove(userEntity);
            return await _context.SaveChangesAsync() > 0;
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
