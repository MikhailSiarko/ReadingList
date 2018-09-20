using System.Collections.Generic;
using ReadingList.Domain.Services;
using ReadingList.WriteModel.Models;
using Xunit;

namespace ReadingList.Tests
{
    public class UpdateTests
    {
        [Fact]
        public void Update_UpdatesPropertiesOfEntityWhichNotMarkedWithIgnoreUpdateAttribute()
        {
            var updateService = new EntityUpdateService();
            const int id = 1;
            const string name = "Default";
            const BookListType type = BookListType.Private;
            const int ownerId = 1;

            var bookList = new BookListWm
            {
                Id = id,
                Name = name,
                Type = type,
                OwnerId = ownerId
            };

            var source = new Dictionary<string, object>
            {
                [nameof(BookListWm.Id)] = 2,
                [nameof(BookListWm.Name)] = "Updated",
                [nameof(BookListWm.Type)] = BookListType.Shared,
                [nameof(BookListWm.OwnerId)] = 2
            };

            updateService.Update(bookList, source);

            Assert.True(bookList.Id == id);
            Assert.True(bookList.Name == (string)source[nameof(BookListWm.Name)]);
            Assert.True(bookList.Type == type);
            Assert.True(bookList.OwnerId == ownerId);
        }
    }
}