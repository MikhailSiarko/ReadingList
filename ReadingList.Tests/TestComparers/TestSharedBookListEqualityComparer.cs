using System.Collections.Generic;
using System.Linq;
using ReadingList.Domain.DTO.BookList;

namespace ReadingList.Tests.TestComparers
{
    public class TestSharedBookListEqualityComparer : IEqualityComparer<SharedBookListDto>
    {
        public bool Equals(SharedBookListDto x, SharedBookListDto y)
        {
            return x.Id == y.Id && string.Equals(x.Name, y.Name) && x.Type == y.Type &&
                   x.OwnerId == y.OwnerId && string.Equals(x.Category, y.Category) &&
                   x.Tags.All(t => y.Tags.Any(o => o == t));
        }

        public int GetHashCode(SharedBookListDto obj)
        {
            unchecked
            {
                var hashCode = obj.Id;
                hashCode = (hashCode * 397) ^ (obj.Name != null ? obj.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) obj.Type;
                hashCode = (hashCode * 397) ^ obj.OwnerId;
                hashCode = (hashCode * 397) ^ (obj.Category != null ? obj.Category.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.Tags != null ? obj.Tags.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}