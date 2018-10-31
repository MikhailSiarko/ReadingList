using System;
using ReadingList.Resources;

namespace ReadingList.Application.Exceptions
{
    public class AccessDeniedException : ApplicationException
    {
        public AccessDeniedException() : base(ExceptionMessages.AccessDenied)
        {
        }
    }
}