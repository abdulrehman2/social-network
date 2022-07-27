using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Contracts.Repositories
{
    public interface IUnitOfWork
    {
        IPostCommentRepository PostComments{ get; }
        IPostRepository Posts{ get; }
        IPostReactRepository PostReacts{ get; }
        int Complete();
    }
}
