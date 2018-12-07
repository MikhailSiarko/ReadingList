using ReadingList.Models.Infrastructure;

namespace ReadingList.Models
{
    public abstract class Entity
    {
        [IgnoreUpdate]
        public int Id { get; set; }
    }
}