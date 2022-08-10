using AutoMapper;
using Identity.Application.Contracts.Services;
using Identity.Application.Dtos.Common;
using Identity.Application.Contracts.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Identity.Application.Contracts.AsyncDataServices;

namespace Identity.Api.Controllers
{
    [Route("api/identity/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenBuilder _tokenBuilder;
        private readonly IMessageBusClient _messageBusClient;

        public AccountController(IMapper mapper, IUserRepository userRepository, ITokenBuilder tokenBuilder, IMessageBusClient messageBusClient)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenBuilder = tokenBuilder;
            _messageBusClient = messageBusClient;
        }

        [HttpPost("signup")]
        public IActionResult SignUp(Application.Dtos.UserManagement.UserCreateDto userDto)
        {
            if (_userRepository.IsUserExist(userDto.Email))
            {
                return BadRequest(new { message = "User with this email already exist" });
            }

            var user = _mapper.Map<Domain.Models.User>(userDto);
            _userRepository.AddUser(user);
            _userRepository.SaveChanges();

            //publish user create event
            _messageBusClient.PublishNewUser(user);

            var userToReturn = _mapper.Map<Application.Dtos.UserManagement.UserReadDto>(user);
            userToReturn.Token = _tokenBuilder.BuildToken(user.Email, user.Id);
            var response = new GenericResponse();
            response.Data = userToReturn;
            response.Message = "Signup successfull";
            return Ok(response);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(Application.Dtos.UserManagement.UserLoginDto userDto)
        {
           
            var response = new GenericResponse();
            if (!_userRepository.IsUserExist(userDto.Email))
            {
                response.Status = System.Net.HttpStatusCode.BadRequest;
                response.Message = "Inavlid Email/Password";
                return Helpers.ObjectResultHelper.GetObjectResult(response);
            }

            var user = _userRepository.Authenticate(userDto.Email, userDto.Password);

            if (user == null)
            {
                response.Status = System.Net.HttpStatusCode.BadRequest;
                response.Message = "Inavlid Email/Password";
                return Helpers.ObjectResultHelper.GetObjectResult(response);
            }

            var userToReturn = _mapper.Map<Application.Dtos.UserManagement.UserReadDto>(user);
            userToReturn.Token = _tokenBuilder.BuildToken(user.Email, user.Id);
            response.Data = userToReturn;
            response.Message = "Login successfull";
            return Helpers.ObjectResultHelper.GetObjectResult(response);

        }


        [HttpGet("verify")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> VerifyToken()
        {
            var username = User
                .Claims
                .SingleOrDefault();

            if (username == null)
            {
                return Unauthorized();
            }

            var userExists = _userRepository.IsUserExist(username.Value);

            if (!userExists)
            {
                return Unauthorized();
            }

            return NoContent();
        }

    }
}
