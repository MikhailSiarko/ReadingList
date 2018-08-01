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
            const string jsonFields = "Fields";
            const int ownerId = 1;

            var bookList = new BookList
            {
                Id = id,
                Name = name,
                Type = type,
                JsonFields = jsonFields,
                OwnerId = ownerId
            };

            var source = new Dictionary<string, object>
            {
                [nameof(BookList.Id)] = 2,
                [nameof(BookList.Name)] = "Updated",
                [nameof(BookList.Type)] = BookListType.Shared,
                [nameof(BookList.JsonFields)] = "New Fields",
                [nameof(BookList.OwnerId)] = 2
            };

            updateService.Update(bookList, source);

            Assert.True(bookList.Id == id);
            Assert.True(bookList.Name == (string)source[nameof(BookList.Name)]);
            Assert.True(bookList.Type == type);
            Assert.True(bookList.JsonFields == (string)source[nameof(BookList.JsonFields)]);
            Assert.True(bookList.OwnerId == ownerId);
        }
    }
}