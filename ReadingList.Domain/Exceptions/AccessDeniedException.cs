using System;
using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class AccessDeniedException : ApplicationException
    {
        public AccessDeniedException() : base(ExceptionMessages.AccessDenied)
        {
        }
    }
}