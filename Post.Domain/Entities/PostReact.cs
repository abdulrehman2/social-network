using Post.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class PostReact:EntityBase
    {
        

        [Required]
        public int PostId { get; set; }
        public Post Post { get; set; }


        [Required]
        public int UserId { get; set; }    
        //public User User { get; set; }


        [Required]
        public int ReactTypeId { get; set; }
        public ReactType ReactType{ get; set; }


        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }


    }
}
