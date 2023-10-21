using AutoMapper;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Requests;
using TaskManagement.Core.Responses;

namespace UserManagement.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<UsersService> _logger;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, ILogger<UsersService> logger, IMapper mapper)
        {
            _usersRepository = usersRepository ?? throw new Exception(nameof(usersRepository));
            _logger = logger ?? throw new Exception(nameof(logger));
            _mapper = mapper ?? throw new Exception(nameof(mapper));
        }

        public async Task<GetUserResponse> GetUser(string userId)
        {
            var userEntity = await _usersRepository.GetUser(userId);

            return userEntity is null ?
                throw new Exception($"Could not find user for id {userId}") :
                _mapper.Map<GetUserResponse>(userEntity);
        }

        public async Task<GetUserResponse> CreateUser(CreateUpdateUserRequest userRequest)
        {
            var userEntity = _mapper.Map<UserEntity>(userRequest);
            var saved = await _usersRepository.CreateUser(userEntity);

            //we can have specific exeption that can be handled in exeption middleware
            return !saved ?
                 throw new Exception("Could not save user") :
                _mapper.Map<GetUserResponse>(userEntity);
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var userEntity = await _usersRepository.GetUser(userId);

            return userEntity is null ?
                throw new Exception($"Could not find user for id {userId}") :
                await _usersRepository.DeleteUser(userEntity);
        }

        public async Task<bool> UpdateUser(string userId, CreateUpdateUserRequest userRequest)
        {
            var userEntity = await _usersRepository.GetUser(userId);

            if (userEntity is not null)
            {
                userEntity.Name = userRequest.Name;
                userEntity.Email = userRequest.Email;

                return await _usersRepository.UpdateUser(userEntity);
            }

            throw new Exception($"Could not find user for id {userId}");
        }
    }
}
