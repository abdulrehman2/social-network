using Post.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class User:EntityBase
    {
        public User()
        {
            //Posts=new List<Post>();
            //PostComments =  new List<PostComment>();
            //PostReacts = new List<PostReact>();
        }

        [Required]
        public int ExternalId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? ProfilePicLocation { get; set; }


        //public ICollection<Post> Posts { get; set; }
        //public ICollection<PostComment> PostComments { get; set; }
        //public ICollection<PostReact> PostReacts { get; set; }

    }
}
