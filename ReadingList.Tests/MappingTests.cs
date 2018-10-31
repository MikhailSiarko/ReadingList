using System;
using AutoMapper;
using ReadingList.Application;
using ReadingList.Domain.Entities;
using ReadingList.Domain.Enumerations;
using ReadingList.Application.DTO.BookList;
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
                Author = "Author",
                Title = "Title",
                Status = BookItemStatus.Reading,
                ReadingTimeInSeconds = Convert.ToInt32(TimeSpan.FromHours(5).TotalSeconds)
            };
            var mapped = Mapper.Map<PrivateBookListItem, PrivateBookListItemDto>(privateListItem);

            Assert.Equal(privateListItem.Id, mapped.Id);
            Assert.Equal(privateListItem.Title, mapped.Title);
            Assert.Equal(privateListItem.Author, mapped.Author);
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
                Author = "Author",
                Title = "Title",
                BookListId = 5,
                GenreId = "stories"
            };
            var mapped = Mapper.Map<SharedBookListItem, SharedBookListItemDto>(sharedBookListItem);

            Assert.Equal(sharedBookListItem.Id, mapped.Id);
            Assert.Equal(sharedBookListItem.Title, mapped.Title);
            Assert.Equal(sharedBookListItem.Author, mapped.Author);
            Assert.Equal(sharedBookListItem.BookListId, mapped.ListId);
            Assert.Equal(sharedBookListItem.GenreId, mapped.GenreId);
        }
    }
}