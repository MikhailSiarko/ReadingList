using System;
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
                ReadingTimeInSeconds = default(int),
                LastStatusUpdateDate = DateTime.Now.AddDays(-3),
                Status = BookItemStatus.ToReading
            };

            var readingTime = item.ReadingTimeInSeconds +
                              ReadingTimeCalculator.Calculate(item.Status, item.LastStatusUpdateDate,
                                  BookItemStatus.Reading);

            Assert.True(readingTime == default(double));
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
                ReadingTimeInSeconds = Convert.ToInt32(TimeSpan.FromMinutes(65).TotalSeconds),
                LastStatusUpdateDate = DateTime.Now.AddDays(-3),
                Status = BookItemStatus.Reading
            };

            var readingTime = item.ReadingTimeInSeconds +
                              ReadingTimeCalculator.Calculate(item.Status, item.LastStatusUpdateDate,
                                  BookItemStatus.Reading);

            Assert.True(item.ReadingTimeInSeconds == readingTime);
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
                ReadingTimeInSeconds = Convert.ToInt32(TimeSpan.FromMinutes(165).TotalSeconds),
                LastStatusUpdateDate = DateTime.Now.AddMinutes(-135),
                Status = BookItemStatus.Reading
            };

            var oldReadingTime = item.ReadingTimeInSeconds;

            var readingTime = item.ReadingTimeInSeconds +
                              ReadingTimeCalculator.Calculate(item.Status, item.LastStatusUpdateDate,
                                  BookItemStatus.Read);

            Assert.True(oldReadingTime + Convert.ToInt32(TimeSpan.FromMinutes(135).TotalSeconds) == readingTime);
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
                ReadingTimeInSeconds = default(int),
                LastStatusUpdateDate = DateTime.Now.AddDays(-3),
                Status = BookItemStatus.Reading
            };

            var readingTime = item.ReadingTimeInSeconds +
                              ReadingTimeCalculator.Calculate(item.Status, item.LastStatusUpdateDate,
                                  BookItemStatus.StartedButPostponed);

            Assert.True(Convert.ToInt32(TimeSpan.FromDays(3).TotalSeconds) == readingTime);
        }
    }
}