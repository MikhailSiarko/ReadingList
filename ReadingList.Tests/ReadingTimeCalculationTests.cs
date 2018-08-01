﻿using System;
using ReadingList.Domain.Services;
using ReadingList.WriteModel.Models;
using Xunit;

namespace ReadingList.Tests
{
    public class ReadingTimeCalculationTests
    {
        [Fact]
        public void Calculate_Returns0_When_BookStatusChangedFromToReadingToReading_And_LastStatusUpdateDateIsNowMinus3Days()
        {
            var item = new PrivateBookListItem
            {
                Author = "Author",
                Title = "Title",
                BookListId = 3,
                Id = 5,
                ReadingTime = default(TimeSpan),
                LastStatusUpdateDate = DateTime.Now.AddDays(-3),
                Status = BookItemStatus.ToReading
            };

            var readingTime = item.ReadingTime +
                              ReadingTimeCalculator.Calculate(item.Status, item.LastStatusUpdateDate,
                                  BookItemStatus.Reading);

            Assert.Equal(default(TimeSpan).Minutes, readingTime.Minutes);
        }
        
        [Fact]
        public void Calculate_Returns0_When_BookStatusChangedFromReadingToReading_And_LastStatusUpdateDateIsNowMinus3Days()
        {
            var item = new PrivateBookListItem
            {
                Author = "Author",
                Title = "Title",
                BookListId = 3,
                Id = 5,
                ReadingTime = TimeSpan.FromMinutes(65),
                LastStatusUpdateDate = DateTime.Now.AddDays(-3),
                Status = BookItemStatus.Reading
            };

            var readingTime = item.ReadingTime +
                              ReadingTimeCalculator.Calculate(item.Status, item.LastStatusUpdateDate,
                                  BookItemStatus.Reading);

            Assert.Equal(item.ReadingTime.Minutes, readingTime.Minutes);
        }
        
        [Fact]
        public void Calculate_Returns135Minutes_When_BookStatusChangedFromReadingToRead_And_LastStatusUpdateDateIsNowMinus135Minutes()
        {
            var item = new PrivateBookListItem
            {
                Author = "Author",
                Title = "Title",
                BookListId = 3,
                Id = 5,
                ReadingTime = TimeSpan.FromMinutes(165),
                LastStatusUpdateDate = DateTime.Now.AddMinutes(-135),
                Status = BookItemStatus.Reading
            };

            var oldReadingTime = item.ReadingTime;

            var readingTime = item.ReadingTime +
                              ReadingTimeCalculator.Calculate(item.Status, item.LastStatusUpdateDate,
                                  BookItemStatus.Read);

            Assert.Equal(oldReadingTime.Add(TimeSpan.FromMinutes(135)).Minutes, readingTime.Minutes);
        }
        
        [Fact]
        public void Calculate_Returns3Days_When_BookStatusChangedFromReadingToStartedButPostponed_And_LastStatusUpdateDateIsNowMinus3Days()
        {
            var item = new PrivateBookListItem
            {
                Author = "Author",
                Title = "Title",
                BookListId = 3,
                Id = 5,
                ReadingTime = default(TimeSpan),
                LastStatusUpdateDate = DateTime.Now.AddDays(-3),
                Status = BookItemStatus.Reading
            };

            var readingTime = item.ReadingTime +
                              ReadingTimeCalculator.Calculate(item.Status, item.LastStatusUpdateDate,
                                  BookItemStatus.StartedButPostponed);

            Assert.Equal(TimeSpan.FromDays(3).Minutes, readingTime.Minutes);
        }
    }
}