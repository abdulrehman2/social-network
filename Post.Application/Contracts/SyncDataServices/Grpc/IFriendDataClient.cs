using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Contracts.SyncDataServices.Grpc
{
    public interface IFriendDataClient
    {
        IEnumerable<Dtos.Friend.UserFriendDto> ReturnAllUserFriends(int userId);
    }
}
