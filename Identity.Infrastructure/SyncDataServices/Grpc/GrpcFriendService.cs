using AutoMapper;
using Grpc.Core;
using Grpc.Protos.Identity;
using Identity.Infrastructure.Repositories;
using Identity.Application.Contracts.Repositories;

namespace Identity.Infrastructure.SyncDataServices.Grpc
{
    public class GrpcFriendService : GrpcFriend.GrpcFriendBase
    {
        private readonly IFriendshipRepository _friendRepository;
        private readonly IMapper _mapper;

        public GrpcFriendService(IFriendshipRepository friendRepository, IMapper mapper)
        {
            _friendRepository = friendRepository;
            _mapper = mapper;
        }

        public override Task<FriendResponse> GetAllFriends(GetAllFriendRequest request, ServerCallContext context)
        {
            var response = new FriendResponse();

            var friends = _friendRepository.GetUserFriends(request.UserId);

            foreach (var friend in friends)
            {
                response.Friend.Add(_mapper.Map<GrpcFriendModel>(friend));
            }

            return Task.FromResult(response);
        }

    }
}
