using System.Collections.Generic;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.MapperProfiles;
using ReadingList.Tests.TestComparers;
using ReadingList.WriteModel.Models;
using Xunit;

namespace ReadingList.Tests
{
    public class MappingTests
    {
        static MappingTests()
        {
            Mapper.Initialize(conf => conf.AddProfile<BookListProfile>());
        }
        [Fact]
        public void SharedBookListDtoToBookListMappingAndReverseMapping()
        {
            var shared = new SharedBookListDto
            {
                Id = 1,
                Name = "My Shared List",
                OwnerId = 2,
                Tags = new List<string> {"fantasy", "awesome", "cool"},
                Type = BookListType.Shared
            };

            var mappedList = Mapper.Map<SharedBookListDto, BookList>(shared);
            var remappedShared = Mapper.Map<BookList, SharedBookListDto>(mappedList);
            Assert.Equal(remappedShared, shared, new TestSharedBookListEqualityComparer());
        }

        [Fact]
        public void PrivateBookListDtoToBookListMappingAndReverseMapping()
        {
            var privateList = new PrivateBookListDto
            {
                Id = 54,
                Name = "My private list",
                OwnerId = 895,
                Type = BookListType.Private
            };
            var mapped = Mapper.Map<PrivateBookListDto, BookList>(privateList);
            var remapped = Mapper.Map<BookList, PrivateBookListDto>(mapped);
            Assert.Equal(privateList, remapped, new PrivateBookListDtoEqualityComparer());
        }
    }
}