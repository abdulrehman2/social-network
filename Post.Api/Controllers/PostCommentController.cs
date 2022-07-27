using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Post.Application.Contracts.Repositories;
using Post.Application.Dtos.Comment;
using Post.Application.Dtos.Common;

namespace Post.Api.Controllers
{
    [Route("api/social/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "IdentityApiKey")]
    public class PostCommentController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostCommentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Application.Dtos.Comment.AddCommentDto addComment)
        {
            var comment = _mapper.Map<Domain.Entities.PostComment>(addComment);
            comment.CreatedBy = User.GetUserId().ToString();
            comment.UserId = User.GetUserId();
            await _unitOfWork.PostComments.Add(comment);
            _unitOfWork.Complete();
            var newAddedComment = _unitOfWork.PostComments.GetPostComments(comment.PostId, comment.Id);
            return Ok(newAddedComment[0]);
        }



        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPostComments(int postId)
        {
            var comments = _unitOfWork.PostComments.GetPostComments(postId);
            return Ok(comments);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] JsonPatchDocument<UpdateCommentDto> patchComment)
        {
            var comment = _unitOfWork.PostComments.Find(x => x.Id == id && x.UserId == User.GetUserId()).FirstOrDefault();
            if (comment == null)
            {
                return Helpers.ObjectResultHelper.GetObjectResult(new GenericResponse { Message = "Comment not found", Status = System.Net.HttpStatusCode.BadRequest });
            }

            var commentToPatch = _mapper.Map<UpdateCommentDto>(comment);
            patchComment.ApplyTo(commentToPatch);
            _mapper.Map(commentToPatch, comment);

            _unitOfWork.PostComments.Update(comment);
            _unitOfWork.Complete();
            return Helpers.ObjectResultHelper.GetObjectResult(new GenericResponse { Message = "Comment edited successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = _unitOfWork.PostComments.Find(x => x.Id == id && x.UserId == User.GetUserId()).FirstOrDefault();
            if (comment == null)
            {
                return Helpers.ObjectResultHelper.GetObjectResult(new GenericResponse { Message = "Comment not found", Status = System.Net.HttpStatusCode.BadRequest });
            }

            _unitOfWork.PostComments.Remove(comment);
            _unitOfWork.Complete();
            return Helpers.ObjectResultHelper.GetObjectResult(new GenericResponse { Message = "Comment deleted successfully" });
        }
    }
}
