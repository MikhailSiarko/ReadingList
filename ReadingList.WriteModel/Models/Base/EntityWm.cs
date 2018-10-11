using System.ComponentModel.DataAnnotations.Schema;
using ReadingList.WriteModel.Infrastructure;

namespace ReadingList.WriteModel.Models.Base
{
    public abstract class EntityWm
    {
        [IgnoreUpdate]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}