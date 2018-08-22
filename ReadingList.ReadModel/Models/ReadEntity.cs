using System;

namespace ReadingList.ReadModel.Models
{
    public abstract class ReadEntity : IEquatable<ReadEntity>
    {
        public int Id { get; set; }

        public bool Equals(ReadEntity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ReadEntity) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}