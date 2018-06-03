using System;
using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class CannotChangeStatusException : ApplicationException
    {
        public CannotChangeStatusException(string from, string to) 
            : base(string.Format(ExceptionMessages.CannotChangeStatusFromTo, from, to))
        {
        }
    }
}