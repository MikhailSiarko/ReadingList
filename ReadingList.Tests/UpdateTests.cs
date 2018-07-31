using System.Collections.Generic;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Services;
using ReadingList.WriteModel.Models;
using Xunit;

namespace ReadingList.Tests
{
    public class UpdateTests
    {
        [Fact]
        public void Update()
        {
            var bookList = new BookList
            {
                Id = 1,
                Name = "Default",
                Type = BookListType.Private,
                JsonFields = "Fields",
                OwnerId = 1
            };

            var source = new Dictionary<string, object>
            {
                ["Id"] = 2,
                ["Name"] = "Updated",
                ["Description"] = "Description",
                ["Type"] = BookListType.Shared,
                ["JsonFields"] = "New Fields",
                ["Owner"] = new User(),
                ["OwnerId"] = 2
            };

            bookList.UpdateExpr(source);

            Assert.True(bookList.Name == (string)source["Name"]);
        }
    }
}