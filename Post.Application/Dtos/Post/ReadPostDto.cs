using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Dtos.Post
{
    public class ReadPostDto
    {
        public int Id { get; set; }
        public string? WrittenText { get; set; }

        public string? PostCreator { get; set; }
        public int PostCreatorId { get; set; }
        public string PostCreatorProfilePicutre { get; set; }
        public string? MediaLocation { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsReacted { get; set; }
        public int ReactTypeId { get; set; }
        public int ReactCount { get; set; }
        public int CommentCount { get; set; }

    }
}
