using System.Collections.Generic;
using ReadingList.Domain.Infrastructure.Extensions;
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
                Name = "Default",
                Type = BookListType.Private
            };

            var source = new Dictionary<string, object>
            {
                ["Name"] = "Updated",
                ["Description"] = "Description"
            };

            bookList.UpdateExpr(source);

            Assert.True(bookList.Name == (string)source["Name"]);
        }
    }
}