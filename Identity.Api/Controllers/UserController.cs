using AutoMapper;
using Identity.Application.Contracts.Repositories;
using Identity.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/identity/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IFriendshipRepository friendshipRepository, IMapper mapper)
        {
            _userRepository= userRepository;
            _friendshipRepository = friendshipRepository;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _mapper.Map<Application.Dtos.UserManagement.UserReadDto>(_userRepository.GetUserById(id));
            user.FriendsCount = _friendshipRepository.GetUserFriends(id).Count();

            return Ok(user);
        }


        [HttpGet("search/{query}")]
        public IActionResult SearchUser(string query)
        {
            return Ok(_userRepository.SearchUser(query));

        }
    }
}
