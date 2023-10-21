using TaskManagement.Core.Entities;
using TaskManagement.Core.Enums;
using TaskManagement.Infrastructure;

namespace TaskManagement.API.Helpers
{
    public static class DbSeederHelper
    {
        private const string UserId1 = "z3f7d6c0-ff7a-4b5a-9b9b-1a2b3c4d5e6f";
        private const string UserId2 = "x3f7d6c0-ff7a-4b5a-3b9b-1a2b3c4d5e6f";
        private const string UserId3 = "y3f7d6c0-ff7a-4b5a-1b9b-1a2b3c4d5e6f";

        private const string TaskId1 = "a3f7d6c0-ff7a-4b5a-9b9b-1a2b3c4d5e6f";
        private const string TaskId2 = "b3f7d6c0-ff7a-4b5a-9b9b-1a2b3c4d5e6f";
        private const string TaskId3 = "c3f7d6c0-ff7a-4b5a-9b9b-1a2b3c4d5e6f";
        private const string TaskId4 = "d3f7d6c0-ff7a-4b5a-9b9b-1a2b3c4d5e6f";
        private const string TaskId5 = "e3f7d6c0-ff7a-4b5a-9b9b-1a2b3c4d5e6f";
        private const string TaskId6 = "f3f7d6c0-ff7a-4b5a-9b9b-1a2b3c4d5e6f";
        private const string TaskId7 = "g3f7d6c0-ff7a-4b5a-9b9b-1a2b3c4d5e6f";

        private static readonly DateTime CreatedDate = new DateTime(2023, 10, 21);

        public static void SeedDb(ApplicationDbContext applicationDbContext)
        {
            applicationDbContext.Users.AddRange(GetMockUsers());
            applicationDbContext.Tasks.AddRange(GetMockTasks());
            applicationDbContext.SaveChanges();
        }
        public static List<UserEntity> GetMockUsers()
        {
            return new List<UserEntity>
            {
                new UserEntity
                {
                    Id = UserId1,
                    Name = "Test user 1",
                    Email = "test@user1.com",
                    CreatedDate = CreatedDate,
                },
                new UserEntity
                {
                    Id = UserId2,
                    Name = "Test user 1",
                    Email = "test@user1.com",
                    CreatedDate = CreatedDate,
                },
                new UserEntity
                {
                    Id = UserId3,
                    Name = "Test user 1",
                    Email = "test@user1.com",
                    CreatedDate = CreatedDate,
                }
            };
        }

        public static List<TaskEntity> GetMockTasks()
        {
            return new List<TaskEntity>
            {
                new TaskEntity
                {
                    Id = TaskId1,
                    Name = "Task name 1",
                    Description = "Dummy description",
                    UserId = UserId1,
                    Status = (int)TaskStatusEnum.New
                },
                new TaskEntity
                {
                    Id = TaskId2,
                    Name = "Task name 2",
                    Description = "Dummy description",
                    UserId = UserId1,
                    Status = (int)TaskStatusEnum.New
                },
                new TaskEntity
                {
                    Id = TaskId3,
                    Name = "Task name 3",
                    Description = "Dummy description",
                    UserId = UserId1,
                    Status = (int)TaskStatusEnum.New
                },

                new TaskEntity
                {
                    Id = TaskId4,
                    Name = "Task name 4",
                    Description = "Dummy description",
                    UserId = UserId1,
                    Status = (int)TaskStatusEnum.New
                },
                new TaskEntity
                {
                    Id = TaskId5,
                    Name = "Task name 5",
                    Description = "Dummy description",
                    UserId = UserId1,
                    Status = (int)TaskStatusEnum.New
                },
                new TaskEntity
                {
                    Id = TaskId6,
                    Name = "Task name 6",
                    Description = "Dummy description",
                    UserId = UserId2,
                    Status = (int)TaskStatusEnum.New
                },
                new TaskEntity
                {
                    Id = TaskId7,
                    Name = "Task name 7",
                    Description = "Dummy description",
                    UserId = UserId3,
                    Status = (int)TaskStatusEnum.New
                }
            };
        }
    }
}
