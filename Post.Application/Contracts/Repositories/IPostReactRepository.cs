using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Contracts.Repositories
{
    public interface IPostReactRepository: IGenericRepository<Domain.Entities.PostReact>
    {
        bool AddReactToPost(int userId, Application.Dtos.React.AddReactDto addReact);
    }
}
