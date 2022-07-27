using Post.Application.Dtos.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Contracts.Repositories
{
    public interface IPostRepository: IGenericRepository<Domain.Entities.Post>
    {
        IEnumerable<Application.Dtos.Post.ReadPostDto> GetMyPosts(int userId);

        IEnumerable<ReadPostDto> GetUserWall(int userId);
    }
}
