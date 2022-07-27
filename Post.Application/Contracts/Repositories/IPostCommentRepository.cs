using Post.Application.Dtos.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Contracts.Repositories
{
    public interface IPostCommentRepository: IGenericRepository<Domain.Entities.PostComment>
    {
        List<CommentReadDto> GetPostComments(int postId, int? commentId=null);
    }
}
