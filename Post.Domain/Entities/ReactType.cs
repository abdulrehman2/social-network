using Post.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class ReactType:EntityBase
    {
        public ReactType()
        {
            Reacts = new List<PostReact>();
        }
        

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        public ICollection<PostReact> Reacts{ get; set; }
    }
}
