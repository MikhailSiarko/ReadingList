using System.Linq;
using ReadingList.Models.Write;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Domain.Infrastructure.Specifications
{
    public class SharedListNameSpecification : Specification<string>
    {
        public SharedListNameSpecification(User user)
            : base(name => user.BookLists
                .Where(b => b.Type == BookListType.Shared)
                .All(b => b.Name != name))
        {
        }
    }
}