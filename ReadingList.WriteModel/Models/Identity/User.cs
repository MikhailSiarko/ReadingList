using ReadingList.WriteModel.Models.Base;

namespace ReadingList.WriteModel.Models
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Profile Profile { get; set; }
    }
}