using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Models
{
    public class AudienceType
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
            



    }
}
