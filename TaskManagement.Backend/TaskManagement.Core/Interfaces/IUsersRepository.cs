using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Interfaces
{
    public interface IUsersRepository
    {
        Task<UserEntity> GetUser(string userId);
    }
}
