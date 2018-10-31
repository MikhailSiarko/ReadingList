namespace ReadingList.Domain.Entities.Base
{
    public abstract class NamedEntity : Entity
    {
        public string Name { get; set; }
    }
}