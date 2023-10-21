using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly ApplicationDbContext _context;

        public TasksRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            SeedData();
        }

        private void SeedData()
        {
            //if the there are no users, seed the database with mock data
            if (_context.Users.Count() == 0)
            {
                _context.AddRange(MockData.GetMockTasks());
                _context.SaveChanges();
            }
        }
    }
}
