using System;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Models.Write;
using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class CannotChangeStatusException : ApplicationException
    {
        public CannotChangeStatusException(BookItemStatus from, BookItemStatus to)
            : base(string.Format(ExceptionMessages.CannotChangeStatusFromTo, from.ToStringFromDescription(),
                to.ToStringFromDescription()))
        {
        }
    }
}