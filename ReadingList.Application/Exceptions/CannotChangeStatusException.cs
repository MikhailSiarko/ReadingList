using System;
using ReadingList.Domain.Enumerations;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Application.Exceptions
{
    public class CannotChangeStatusException : ApplicationException
    {
        public CannotChangeStatusException(BookItemStatus from, BookItemStatus to) 
            : base(string.Format(ExceptionMessages.CannotChangeStatusFromTo, from.ToStringFromDescription(), to.ToStringFromDescription()))
        {
        }
    }
}