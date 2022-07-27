using Identity.Application.Dtos.Common;
using System.Collections.Generic;

namespace Identity.Application.Contracts.Repositories
{
    public interface IFriendshipRepository:IGenericRepository<Domain.Models.FriendShipRequest>
    {
        IEnumerable<Dtos.FriendRequest.FriendRequesReadtDto> GetPendingFriendRequests(int userId);
        IEnumerable<Dtos.Friend.UserFriendDto> GetUserFriends(int userId);
        IEnumerable<Dtos.Friend.FriendSuggestionDto> GetFriendSuggestions(int userId);

        GenericResponse AddFriendRequest(int userId,Dtos.FriendRequest.AddFriendRequestDto addFriendRequest);
        GenericResponse AcceptFriendRequest(int userId, Dtos.FriendRequest.AcceptFriendRequestDto acceptFriendRequest);



    }
}
