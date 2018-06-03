using System;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.Services
{
    public class PrivateBookListItemStatusValidator
    {
        public static void Validate(BookItemStatus itemStatus, BookItemStatus newStatus)
        {
            switch (itemStatus)
            {
                case BookItemStatus.Read:
                    if (newStatus == BookItemStatus.Read)
                        break;
                    throw new CannotChangeStatusException(itemStatus.ToStringFromDescription(),
                        newStatus.ToStringFromDescription());
                case BookItemStatus.Reading:
                case BookItemStatus.StartedButPostponed:
                    if (newStatus == BookItemStatus.ToReading)
                        throw new CannotChangeStatusException(itemStatus.ToStringFromDescription(),
                            newStatus.ToStringFromDescription());
                    break;
                case BookItemStatus.ToReading:
                    if (newStatus == BookItemStatus.Read || newStatus == BookItemStatus.StartedButPostponed)
                        throw new CannotChangeStatusException(itemStatus.ToStringFromDescription(),
                            newStatus.ToStringFromDescription());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(itemStatus), itemStatus, null);
            }
        }
    }
}