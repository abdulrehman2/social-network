using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post.Application.Contracts.Repositories;

namespace Post.Api.Controllers
{
    [Route("api/social/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "IdentityApiKey")]
    public class PostReactController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostReactController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddReact(Application.Dtos.React.AddReactDto addReact)
        {
            var result = _unitOfWork.PostReacts.AddReactToPost(User.GetUserId(), addReact);
            _unitOfWork.Complete();
            return Ok();
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostReacts(int id)
        {
            //_unitOfWork.Posts.Add(_mapper.Map<Domain.Entities.Post>(addPost));
            //_unitOfWork.Complete();

            return Ok();
        }
    }
}
