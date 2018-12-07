namespace ReadingList.Models.Read
{
    public class UserDto : Entity
    {
        public string Login { get; set; }

        public int ProfileId { get; set; }

        public string Role { get; set; }

        public string Password { get; set; }
    }
}