﻿using ReadingList.Domain.Entities;
using ReadingList.Domain.Enumerations;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Services.Validation;
using Xunit;

namespace ReadingList.Tests
{
    public class ItemStatusValidationTests
    {
        [Fact]
        public void Validate_ReturnsCantChangeStatusFromReadToReading_When_OldStatusIsReadAndNewStatusIsReading()
        {
            var item = new PrivateBookListItem
            {
                Status = BookItemStatus.Read
            };

            var ex = Assert.Throws<CannotChangeStatusException>(() =>
                PrivateBookListItemStatusValidator.Validate(item.Status, BookItemStatus.Reading));
            Assert.Equal("Can't change status from Read to Reading", ex.Message);
        }
        
        [Fact]
        public void Validate_ReturnsCantChangeStatusFromReadingToToReading_When_OldStatusIsReadingAndNewStatusIsToReading()
        {
            var item = new PrivateBookListItem
            {
                Status = BookItemStatus.Reading
            };

            var ex = Assert.Throws<CannotChangeStatusException>(() =>
                PrivateBookListItemStatusValidator.Validate(item.Status, BookItemStatus.ToReading));
            Assert.Equal("Can't change status from Reading to To Reading", ex.Message);
        }
        
        [Fact]
        public void Validate_ReturnsCantChangeStatusFromStartedButPostponedToToReading_When_OldStatusIsStartedButPostponedAndNewStatusIsToReading()
        {
            var item = new PrivateBookListItem
            {
                Status = BookItemStatus.StartedButPostponed
            };

            var ex = Assert.Throws<CannotChangeStatusException>(() =>
                PrivateBookListItemStatusValidator.Validate(item.Status, BookItemStatus.ToReading));
            Assert.Equal("Can't change status from Started But Postponed to To Reading", ex.Message);
        }
    }
}