using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Profiles
{
    public class PostCommentProfile : Profile
    {
        public PostCommentProfile()
        {
            CreateMap<Dtos.Comment.AddCommentDto, Domain.Entities.PostComment>();
            CreateMap<Dtos.Comment.UpdateCommentDto, Domain.Entities.PostComment>();
            CreateMap<Domain.Entities.PostComment,Dtos.Comment.UpdateCommentDto>();
        }
    }
}
