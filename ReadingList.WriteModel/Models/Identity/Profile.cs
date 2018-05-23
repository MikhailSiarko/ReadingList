namespace ReadingList.WriteModel.Models
{
    public class Profile
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }
    }
}