using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post.Application.Contracts.Repositories;
using Post.Application.Contracts.Services;
using Post.Application.Dtos.Common;
using System.Collections.Generic;
using System.Security.Claims;

namespace Post.Api.Controllers
{
    [Route("api/social/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "IdentityApiKey")]
    public class PostController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;

        public PostController(IUnitOfWork unitOfWork, IMapper mapper, IFileStorage fileStorage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        [HttpGet("user_posts/{id}")]
        public IActionResult GetPosts(int id)
        {
            var myPosts = _unitOfWork.Posts.GetMyPosts(id);
            return Ok(myPosts);
        }


        [HttpGet("user_wall")]
        public IActionResult GetUserWall()
        {
            var response = new GenericResponse();
            var myPosts = _unitOfWork.Posts.GetUserWall(User.GetUserId());
            return Ok(myPosts);
        }



        [HttpPost]
        public async Task<IActionResult> AddPost([FromForm] Application.Dtos.Post.AddPostDto addPost)
        {
            var userId = User.GetUserId();
            try
            {
                var post = _mapper.Map<Domain.Entities.Post>(addPost);
                post.UserId = Convert.ToInt32(userId);
                if (addPost.Media != null)
                {
                    var fileResponse = _fileStorage.SaveFile(addPost.Media);

                    if (fileResponse.IsUploaded)
                        post.MediaLocation = fileResponse.FilePath;
                    else
                        return BadRequest("media upload failed");
                }

                await _unitOfWork.Posts.Add(post);
                _unitOfWork.Complete();
            }
            catch (Exception e)
            {

            }
            return Ok();
        }





    }
}
