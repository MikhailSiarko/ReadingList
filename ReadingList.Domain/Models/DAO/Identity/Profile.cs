namespace ReadingList.Domain.Models.DAO.Identity
{
    public class Profile : Entity
    {
        public string Email { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }
    }
}