using System.Linq;
using ReadingList.Domain.Models.DAO;

namespace ReadingList.Domain.Infrastructure.Specifications
{
    public class BookListAccessSpecification : Specification<int>
    {
        public BookListAccessSpecification(BookList list)
            : base(userId => list.OwnerId == userId || list.BookListModerators.Any(m => m.UserId == userId))
        {
        }
    }
}