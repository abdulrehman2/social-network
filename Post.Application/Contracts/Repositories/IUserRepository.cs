using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Contracts.Repositories
{
    public interface IUserRepository : IGenericRepository<Domain.Entities.User>
    {
        bool SyncMissingUsers(IEnumerable<Domain.Entities.User> user);
    }
}
