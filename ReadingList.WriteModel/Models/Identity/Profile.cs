using ReadingList.WriteModel.Models.Base;

namespace ReadingList.WriteModel.Models
{
    public class Profile : Entity
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}