using System;
using AutoMapper;
using ReadingList.Domain;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DTO.BookLists;
using Xunit;

namespace ReadingList.Tests
{
    public class MappingTests
    {
        static MappingTests()
        {
            Mapper.Initialize(conf =>
                conf.AddProfiles(typeof(JwtBearerConfigurator).Assembly));
        }

        [Fact]
        public void Map_ReturnsPrivateBookListDto_When_PrivateBookListIsMapped()
        {
            var privateList = new BookList
            {
                Id = 54,
                Name = "My private list",
                OwnerId = 895
            };
            var mapped = Mapper.Map<BookList, PrivateBookListDto>(privateList);
            Assert.Equal(privateList.Id, mapped.Id);
            Assert.Equal(privateList.Name, mapped.Name);
            Assert.Equal(privateList.OwnerId, mapped.OwnerId);
        }

        [Fact]
        public void Map_ReturnsPrivateBookListItemDto_When_PrivateBookListItemIsMapped()
        {
            var privateListItem = new PrivateBookListItem
            {
                Id = 54,
                Book = new Book
                {
                    Author = "Author",
                    Title = "Title"
                },
                Status = BookItemStatus.Reading,
                ReadingTimeInSeconds = Convert.ToInt32(TimeSpan.FromHours(5).TotalSeconds)
            };
            var mapped = Mapper.Map<PrivateBookListItem, PrivateBookListItemDto>(privateListItem);

            Assert.Equal(privateListItem.Id, mapped.Id);
            Assert.Equal(privateListItem.Book.Title, mapped.Title);
            Assert.Equal(privateListItem.Book.Author, mapped.Author);
            Assert.Equal((int) privateListItem.Status, mapped.Status);
            Assert.Equal(privateListItem.ReadingTimeInSeconds, mapped.ReadingTimeInSeconds);
        }
        
        [Fact]
        public void Map_ReturnsSharedBookListDto_When_SharedBookListIsMapped()
        {
            var sharedList = new BookList
            {
                Id = 54,
                Name = "My private list",
                OwnerId = 895
            };
            var mapped = Mapper.Map<BookList, SharedBookListPreviewDto>(sharedList);
            Assert.Equal(sharedList.Id, mapped.Id);
            Assert.Equal(sharedList.Name, mapped.Name);
            Assert.Equal(sharedList.OwnerId, mapped.OwnerId);
        }
        
        [Fact]
        public void Map_ReturnsSharedBookListItem_When_SharedBookListItemIsMapped()
        {
            var sharedBookListItem = new SharedBookListItem
            {
                Id = 54,
                Book = new Book
                {
                    Author = "Author",
                    Title = "Title",
                    GenreId = "stories"
                },
                BookListId = 5
            };
            var mapped = Mapper.Map<SharedBookListItem, SharedBookListItemDto>(sharedBookListItem);

            Assert.Equal(sharedBookListItem.Id, mapped.Id);
            Assert.Equal(sharedBookListItem.Book.Title, mapped.Title);
            Assert.Equal(sharedBookListItem.Book.Author, mapped.Author);
            Assert.Equal(sharedBookListItem.BookListId, mapped.ListId);
            Assert.Equal(sharedBookListItem.Book.GenreId, mapped.GenreId);
        }
    }
}