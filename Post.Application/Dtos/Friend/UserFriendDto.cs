using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Dtos.Friend
{
    public class UserFriendDto
    {
        public int  UserId { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
    }
}
