using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Models
{
    public class FriendShipRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public int SenderId { get; set; }

        [ForeignKey("SenderId")]
        public User SenderUser{ get; set; }

        public int ReceiverId { get; set; }

        [ForeignKey("ReceiverId")]
        public User ReceiverUser { get; set; }

        public bool IsAccepted { get; set; }
        public bool IsReported { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime AccpetedDate { get; set; }
        public DateTime ReportedDate { get; set; }

    }
}
