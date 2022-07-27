using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Profiles
{
    public class PostProfile:Profile
    {
        public PostProfile()
        {
            CreateMap<Dtos.Post.AddPostDto, Domain.Entities.Post>()
                .ForMember(x => x.Comments, opt => opt.Ignore())
                .ForMember(x => x.Reactes, opt => opt.Ignore());

            //CreateMap<Domain.Entities.Post, Dtos.Post.ReadPostDto>();
                //.ForMember(x => x.PostCreator, opt => opt.MapFrom(src => src.User.Name))
                //.ForMember(x => x.PostCreatorId, opt => opt.MapFrom(src => src.User.Id));
        }
    }
}
