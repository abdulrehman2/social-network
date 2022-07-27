using Post.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class PostComment: EntityBase
    {
        
        [Required]
        public int PostId{ get; set; }
        public Post Post { get; set; }

        [Required]
        public int UserId { get; set; }
        //public User User { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comment { get; set; }

       

    }
}
