using System;
using ReadingList.Domain.Enumerations;
using ReadingList.Application.Exceptions;

namespace ReadingList.Application.Services.Validation
{
    public static class PrivateBookListItemStatusValidator
    {
        public static void Validate(BookItemStatus itemStatus, BookItemStatus newStatus)
        {
            switch (itemStatus)
            {
                case BookItemStatus.Read:
                    if (newStatus == BookItemStatus.Read)
                        break;
                    throw new CannotChangeStatusException(itemStatus, newStatus);
                case BookItemStatus.Reading:
                case BookItemStatus.StartedButPostponed:
                    if (newStatus == BookItemStatus.ToReading)
                        throw new CannotChangeStatusException(itemStatus, newStatus);
                    break;
                case BookItemStatus.ToReading:
                    if (newStatus == BookItemStatus.Read || newStatus == BookItemStatus.StartedButPostponed)
                        throw new CannotChangeStatusException(itemStatus, newStatus);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(itemStatus), itemStatus.ToString("G"), null);
            }
        }
    }
}