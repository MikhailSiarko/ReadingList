using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingList.WriteModel.Models
{
    public class Profile
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public User User { get; set; }
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}