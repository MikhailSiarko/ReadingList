namespace ReadingList.ReadModel.Models
{
    public class UserRm : EntityRm
    {
        public string Login { get; set; }

        public int ProfileId { get; set; }

        public string Role { get; set; }

        public string Password { get; set; }
    }
}