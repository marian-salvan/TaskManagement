using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Requests;

namespace TaskManagement.API.Controllers
{
    //TODO: add security to the controller (authorize)
    public class UsersController : ODataController
    {
        //TODO: logs
        private readonly ILogger<UsersController> _logger;
        //if the application was bigger, we would have a service layer
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUpdateUserRequest> _requestValidator;

        public UsersController(ILogger<UsersController> logger, IUsersRepository usersRepository,
            IMapper mapper, IValidator<CreateUpdateUserRequest> requestValidator)
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _requestValidator = requestValidator ?? throw new ArgumentNullException(nameof(requestValidator));
        }

        public async Task<IActionResult> Get([FromRoute] string key)
        {
            var user = await _usersRepository.GetUser(key);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        public async Task<IActionResult> Post([FromBody] CreateUpdateUserRequest createRequest)
        {
            var validationResult = _requestValidator.Validate(createRequest);
            if (!validationResult.IsValid)
            {
                return BadRequest(string.Join(";", validationResult.Errors.Select(x => x.ErrorMessage)));
            }

            var userEntity = _mapper.Map<UserEntity>(createRequest);

            var result = await _usersRepository.CreateUser(userEntity);
            if (!result)
            {
                return BadRequest();
            }

            return Created(userEntity);
        }

        public async Task<IActionResult> Put([FromRoute] string key,
            [FromBody] CreateUpdateUserRequest updateRequest)
        {
            var validationResult = _requestValidator.Validate(updateRequest);
            if (!validationResult.IsValid)
            {
                return BadRequest(string.Join(";", validationResult.Errors.Select(x => x.ErrorMessage)));
            }

            var userEntity = await _usersRepository.GetUser(key);

            if (userEntity is null)
            {
                return NotFound();
            }

            userEntity.Name = updateRequest.Name;
            userEntity.Email = updateRequest.Email;

            await _usersRepository.UpdateUser(userEntity);

            return Updated(userEntity);
        }

        public async Task<IActionResult> Delete([FromRoute] string key)
        {
            var userEntity = await _usersRepository.GetUser(key);

            if (userEntity is not null)
            {
                await _usersRepository.DeleteUser(userEntity);
            }

            return NoContent();
        }
    }
}
