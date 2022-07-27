namespace Identity.Application.Dtos.UserManagement
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public string Token { get; set; }

        public int FriendsCount { get; set; }
    }
}
