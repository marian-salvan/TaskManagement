using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Interfaces
{
    public interface IUsersRepository
    {
        Task<UserEntity> GetUser(string userId);
        Task<bool> CreateUser(UserEntity userEntity);
        Task<bool> UpdateUser(UserEntity userEntity);
        Task<bool> DeleteUser(UserEntity userEntity);
        IQueryable<UserEntity> GetAllUsers();
    }
}
