using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingList.WriteModel.Models.Base
{
    public abstract class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
    }
}