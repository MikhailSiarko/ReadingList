using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;
using ReadingList.Models.Write.HelpEntities;

namespace ReadingList.Domain.Services
{
    public class BookListService : IBookListService
    {
        private readonly IDataStorage _dataStorage;

        public BookListService(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public async Task<IEnumerable<SharedBookListTag>> ProcessTags(IEnumerable<Tag> tags, int listId)
        {
            if (tags == null)
            {
                return null;
            }

            var enumerable = tags.ToList();

            var existingTags = enumerable
                .Where(t => t.Id != default(int))
                .ToList();

            var newTags = enumerable
                .Where(t => t.Id == default(int))
                .ToList();

            if (newTags.Any())
            {
                await _dataStorage.SaveBatchAsync(newTags);
            }

            return existingTags
                .Concat(newTags)
                .Select(t => new SharedBookListTag
                {
                    TagId = t.Id,
                    SharedBookListId = listId
                })
                .ToList();
        }
    }
}