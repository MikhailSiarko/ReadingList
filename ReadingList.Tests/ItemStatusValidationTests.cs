using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Validators;
using ReadingList.Models.Write;
using ReadingList.Resources;
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

            Assert.Equal(
                ExceptionMessages.CannotChangeStatusFromTo.F(item.Status.ToString("G"),
                    BookItemStatus.Reading.ToString("G")), ex.Message);
        }

        [Fact]
        public void
            Validate_ReturnsCantChangeStatusFromReadingToToReading_When_OldStatusIsReadingAndNewStatusIsToReading()
        {
            var item = new PrivateBookListItem
            {
                Status = BookItemStatus.Reading
            };

            var ex = Assert.Throws<CannotChangeStatusException>(() =>
                PrivateBookListItemStatusValidator.Validate(item.Status, BookItemStatus.ToReading));
            Assert.Equal(ExceptionMessages.CannotChangeStatusFromTo.F(item.Status.ToString("G"),
                BookItemStatus.ToReading.ToStringFromDescription()), ex.Message);
        }

        [Fact]
        public void
            Validate_ReturnsCantChangeStatusFromStartedButPostponedToToReading_When_OldStatusIsStartedButPostponedAndNewStatusIsToReading()
        {
            var item = new PrivateBookListItem
            {
                Status = BookItemStatus.StartedButPostponed
            };

            var ex = Assert.Throws<CannotChangeStatusException>(() =>
                PrivateBookListItemStatusValidator.Validate(item.Status, BookItemStatus.ToReading));
            Assert.Equal(ExceptionMessages.CannotChangeStatusFromTo.F(item.Status.ToStringFromDescription(),
                BookItemStatus.ToReading.ToStringFromDescription()), ex.Message);
        }
    }
}