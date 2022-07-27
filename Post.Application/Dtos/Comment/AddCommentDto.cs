using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Dtos.Comment
{
    public class AddCommentDto
    {
        public int PostId { get; set; }

        public string Comment { get; set; }

        public IFormFile? Media { get; set; }

    }
}
