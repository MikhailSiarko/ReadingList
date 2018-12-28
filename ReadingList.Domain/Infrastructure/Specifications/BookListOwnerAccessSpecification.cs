using ReadingList.Models.Write;

namespace ReadingList.Domain.Infrastructure.Specifications
{
    public class BookListOwnerAccessSpecification : Specification<int>
    {
        public BookListOwnerAccessSpecification(BookList list) : base(userId => list.OwnerId == userId)
        {
        }
    }
}