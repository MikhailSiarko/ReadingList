using ReadingList.Domain.Entities.Base;

namespace ReadingList.Domain.Entities.Identity
{
    public class Profile : Entity
    {
        public string Email { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }
    }
}