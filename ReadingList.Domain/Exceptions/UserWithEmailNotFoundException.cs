using System;
using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class UserWithEmailNotFoundException : ApplicationException
    {
        public UserWithEmailNotFoundException() : base(ExceptionMessages.UserWithEmailNotFound)
        {
        }
    }
}