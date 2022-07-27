using Post.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class Post:EntityBase
    {
        public Post()
        {
            Comments = new List<PostComment>();
            Reactes = new List<PostReact>();
        }

        [Required]
        public int UserId { get; set; }
        //public User User { get; set; }

        [MaxLength(2000)]
        public string? WrittenText { get; set; }

        [MaxLength(500)]
        public string? MediaLocation { get; set; }

        
        public ICollection<PostComment> Comments { get; set; }
        public ICollection<PostReact> Reactes { get; set; }
    }
}
