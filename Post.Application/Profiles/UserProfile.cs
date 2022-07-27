using AutoMapper;
using Grpc.Protos.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<GrpcUserModel, Domain.Entities.User>()
                .ForMember(x => x.ExternalId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.ProfilePicLocation, opt => opt.MapFrom(src => src.ProfilePicture));
        }
    }
}
