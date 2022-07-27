using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Profiles
{
    public class PostReactProfile:Profile
    {
        public PostReactProfile()
        {
            CreateMap<Dtos.React.AddReactDto, Domain.Entities.PostReact>();
        }
    }
}
