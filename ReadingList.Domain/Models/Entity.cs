using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Models
{
    public abstract class Entity
    {
        [IgnoreUpdate]
        public int Id { get; set; }
    }
}