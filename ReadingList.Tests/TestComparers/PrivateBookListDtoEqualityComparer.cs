using System.Collections.Generic;
using ReadingList.Domain.DTO.BookList;

namespace ReadingList.Tests.TestComparers
{
    public class PrivateBookListDtoEqualityComparer : IEqualityComparer<PrivateBookListDto>
    {
        public bool Equals(PrivateBookListDto x, PrivateBookListDto y)
        {
            return x.Id == y.Id && string.Equals(x.Name, y.Name) &&
                   x.OwnerId == y.OwnerId;
        }

        public int GetHashCode(PrivateBookListDto obj)
        {
            unchecked
            {
                var hashCode = obj.Id;
                hashCode = (hashCode * 397) ^ (obj.Name != null ? obj.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ obj.OwnerId;
                return hashCode;
            }
        }
    }
}