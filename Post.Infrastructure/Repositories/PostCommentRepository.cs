using Post.Application.Contracts.Repositories;
using Post.Application.Dtos.Comment;
using Post.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infrastructure.Repositories
{
    public class PostCommentRepository : GenericRepository<Domain.Entities.PostComment>, IPostCommentRepository
    {
        public PostCommentRepository(AppDbContext context) : base(context)
        {

        }

        public List<CommentReadDto> GetPostComments(int postId, int? commentId = null)
        {
            var comments = new List<CommentReadDto>();
            try
            {
                comments = _context.PostComments.Join(_context.Users,
                  postComment => postComment.UserId,
                  user => user.ExternalId,
                  (postComment, user) => new CommentReadDto
                  {
                      PostId=postComment.PostId,
                      Id = postComment.Id,
                      CreatedDate = postComment.CreatedDate,
                      CommentCreator = user.Name,
                      CommentCreatorId = user.Id,
                      CommentCreatorProfilePicutre = user.ProfilePicLocation,
                      MediaLocation = "",
                      Comment = postComment.Comment,

                  }).Where(postComment => postComment.PostId == postId && postComment.Id == (commentId == null ? postComment.Id : commentId.Value)).OrderByDescending(x => x.CreatedDate).ToList();
            }
            catch (Exception ex)
            {

            }

            return comments;

        }


    }
}
