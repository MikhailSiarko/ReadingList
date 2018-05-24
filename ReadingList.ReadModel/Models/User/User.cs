namespace ReadingList.ReadModel.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int ProfileId { get; set; }
        public int RoleId { get; set; }
    }
}