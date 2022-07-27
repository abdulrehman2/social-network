using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Dtos.Comment
{
    public class CommentReadDto
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CommentCreator { get; set; }

        public int CommentCreatorId { get; set; }

        public string CommentCreatorProfilePicutre { get; set; }

        public string MediaLocation { get; set; }

        public string Comment { get; set; }


    }
}
