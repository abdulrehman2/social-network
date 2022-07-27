using AutoMapper;
using Grpc.Protos.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Profiles
{
    public class FriendProfile : Profile
    {
        public FriendProfile()
        {
            CreateMap<GrpcFriendModel, Dtos.Friend.UserFriendDto>().
                ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name)).
                ForMember(x => x.UserId, opt => opt.MapFrom(x => x.UserId)).
                ForMember(x => x.ProfilePicture, opt => opt.MapFrom(x => x.ProfilePicture));


        }
    }
}
