using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Models
{
    public class UserPrivacy
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User UserForPrivacy { get; set; }


        public int PrivacyRuleId { get; set; }

        [ForeignKey("PrivacyRuleId")]
        public PrivacyRule Rule { get; set; }


        public int AudienceTypeId { get; set; }

        [ForeignKey("AudienceTypeId")]
        public AudienceType Audience{ get; set; }



    }
}
