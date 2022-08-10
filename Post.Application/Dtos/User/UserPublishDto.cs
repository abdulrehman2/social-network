using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Dtos.User
{
    public class UserPublishDto
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public string Event { get; set; }

    }
}
