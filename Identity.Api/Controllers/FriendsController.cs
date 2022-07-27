using Identity.Application.Dtos.Common;
using Identity.Application.Contracts.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/identity/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "IdentityApiKey")]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendshipRepository _friendshipRepository;

        public FriendsController(IFriendshipRepository friendshipRepository)
        {
            _friendshipRepository = friendshipRepository;

        }
        [HttpGet("suggestions")]
        public IActionResult GetSuggestedFriends()
        {
            return Ok(_friendshipRepository.GetFriendSuggestions(User.GetUserId()));
        }

        [HttpGet("friends/{id}")]
        public IActionResult GetUserFriends(int id)
        {
            return Ok(_friendshipRepository.GetUserFriends(id));

        }


        [HttpGet("pending_requests")]
        public IActionResult GetPendingFriendRequests()
        {
            var response = new GenericResponse();
            response.Data = _friendshipRepository.GetPendingFriendRequests(User.GetUserId());
            return Helpers.ObjectResultHelper.GetObjectResult(response);
        }



        [HttpPost("add_friend_request")]
        public IActionResult AddFriendRequest(Application.Dtos.FriendRequest.AddFriendRequestDto friendRequestDto)
        {
            return Helpers.ObjectResultHelper.GetObjectResult(_friendshipRepository.AddFriendRequest(User.GetUserId(), friendRequestDto));
        }

        [HttpPost("accept_request")]
        public IActionResult GetFriendRequest(Application.Dtos.FriendRequest.AcceptFriendRequestDto acceptFriendRequest)
        {
            var response = _friendshipRepository.AcceptFriendRequest(User.GetUserId(), acceptFriendRequest);
            return Helpers.ObjectResultHelper.GetObjectResult(response);
        }
    }
}
