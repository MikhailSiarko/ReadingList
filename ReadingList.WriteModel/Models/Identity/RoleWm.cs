using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingList.WriteModel.Models
{
    public class RoleWm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}