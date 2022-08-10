using AutoMapper;
using Grpc.Protos.Identity;
using Identity.Application.Dtos.UserManagement;
using Identity.Domain.Models;

namespace Identity.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserReadDto>().ForMember(x => x.Token, opt => opt.Ignore())
                .ForMember(x => x.ProfilePicture, opt => opt.MapFrom(x => x.ProfilePicLocation));
            CreateMap<User, GrpcUserModel>().
              ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id)).
              ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)).
              ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicLocation));


            CreateMap<User, UserPublishDto>().ForMember(x => x.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
        }

    }
}
