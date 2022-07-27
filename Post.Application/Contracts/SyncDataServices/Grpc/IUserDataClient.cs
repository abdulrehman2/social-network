using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Contracts.SyncDataServices.Grpc
{
    public interface IUserDataClient
    {
        IEnumerable<Domain.Entities.User> ReturnAllUsers();
    }
}
