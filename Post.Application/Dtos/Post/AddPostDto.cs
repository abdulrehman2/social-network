using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Dtos.Post
{
    public class AddPostDto
    {
        public IFormFile? Media{ get; set; }
        public string WrittenText { get; set; }
        //public string MediaLocation { get; set; }
    }
}
