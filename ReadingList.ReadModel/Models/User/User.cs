namespace ReadingList.ReadModel.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public int ProfileId { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}