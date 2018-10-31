using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Entities.Base
{
    public abstract class Entity
    {
        [IgnoreUpdate]
        public int Id { get; set; }
    }
}