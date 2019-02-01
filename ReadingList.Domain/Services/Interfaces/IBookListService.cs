using System.Collections.Generic;
using System.Threading.Tasks;
using ReadingList.Models.Write;
using ReadingList.Models.Write.HelpEntities;

namespace ReadingList.Domain.Services.Interfaces
{
    public interface IBookListService
    {
        Task<IEnumerable<SharedBookListTag>> ProcessTags(IEnumerable<Tag> tags, int listId);
    }
}