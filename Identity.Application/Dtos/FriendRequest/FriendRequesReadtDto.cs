namespace Identity.Application.Dtos.FriendRequest
{
    public class FriendRequesReadtDto
    {
        public Guid RequestId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfiePicture { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsAccepted{ get; set; }
    }
}
