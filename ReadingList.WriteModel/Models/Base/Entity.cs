using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingList.WriteModel.Models.Base
{
    public abstract class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}