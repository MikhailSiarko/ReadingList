using System;
using System.Linq;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.ReadModel.Models;
using ReadingList.WriteModel.Models;
using Xunit;

namespace ReadingList.Tests
{
    public class MappingTests
    {
        static MappingTests()
        {
            Mapper.Initialize(conf =>
                conf.CreateMap<SimplifiedSharedBookListRm, SimplifiedSharedBookListDto>());
        }

        [Fact]
        public void Map_ReturnsPrivateBookListDto_When_PrivateBookListIsMapped()
        {
            var privateList = new PrivateBookListRm
            {
                Id = 54,
                Name = "My private list",
                OwnerId = 895
            };
            var mapped = Mapper.Map<PrivateBookListRm, PrivateBookListDto>(privateList);
            Assert.Equal(privateList.Id, mapped.Id);
            Assert.Equal(privateList.Name, mapped.Name);
            Assert.Equal(privateList.OwnerId, mapped.OwnerId);
        }

        [Fact]
        public void Map_ReturnsPrivateBookListItemDto_When_PrivateBookListItemIsMapped()
        {
            var privateListItem = new PrivateBookListItemRm
            {
                Id = 54,
                Author = "Author",
                Title = "Title",
                Status = (int)BookItemStatus.Reading,
                ReadingTimeInSeconds = Convert.ToInt32(TimeSpan.FromHours(5).TotalSeconds)
            };
            var mapped = Mapper.Map<PrivateBookListItemRm, PrivateBookListItemDto>(privateListItem);

            Assert.Equal(privateListItem.Id, mapped.Id);
            Assert.Equal(privateListItem.Title, mapped.Title);
            Assert.Equal(privateListItem.Author, mapped.Author);
            Assert.Equal(privateListItem.Status, mapped.Status);
            Assert.Equal(privateListItem.ReadingTimeInSeconds, mapped.ReadingTimeInSeconds);
        }
        
        [Fact]
        public void Map_ReturnsSharedBookListDto_When_SharedBookListIsMapped()
        {
            var testObj = new {Category = "Stories", Tags = new[] {"story"}};
            var sharedList = new SimplifiedSharedBookListRm
            {
                Id = 54,
                Name = "My private list",
                OwnerId = 895,
                Tags = testObj.Tags
            };
            var mapped = Mapper.Map<SimplifiedSharedBookListRm, SimplifiedSharedBookListDto>(sharedList);
            Assert.Equal(sharedList.Id, mapped.Id);
            Assert.Equal(sharedList.Name, mapped.Name);
            Assert.Equal(sharedList.OwnerId, mapped.OwnerId);
            Assert.False(mapped.Tags.Except(testObj.Tags).Any());
        }
        
        [Fact]
        public void Map_ReturnsSharedBookListItem_When_SharedBookListItemIsMapped()
        {
            var sharedBookListItem = new SharedBookListItemRm
            {
                Id = 54,
                Author = "Author",
                Title = "Title",
                ListId = 5,
                GenreId = "stories"
            };
            var mapped = Mapper.Map<SharedBookListItemRm, SharedBookListItemDto>(sharedBookListItem);

            Assert.Equal(sharedBookListItem.Id, mapped.Id);
            Assert.Equal(sharedBookListItem.Title, mapped.Title);
            Assert.Equal(sharedBookListItem.Author, mapped.Author);
            Assert.Equal(sharedBookListItem.ListId, mapped.ListId);
            Assert.Equal(sharedBookListItem.GenreId, mapped.GenreId);
        }
    }
}