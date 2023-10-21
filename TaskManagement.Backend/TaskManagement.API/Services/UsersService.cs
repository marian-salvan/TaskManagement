using AutoMapper;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<UsersService> _logger;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, ILogger<UsersService> logger, IMapper mapper)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
