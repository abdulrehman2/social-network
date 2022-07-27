using AutoMapper;
using Grpc.Protos.Identity;

namespace Identity.Application.Profiles
{
    public class FriendProfile : Profile
    {
        public FriendProfile()
        {
            CreateMap<Dtos.Friend.UserFriendDto, GrpcFriendModel>();
        }
    }
}
