using System;
using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class WrongPasswordException : ApplicationException
    {
        public WrongPasswordException() : base(ExceptionMessages.WrongPassword)
        {
        }
    }
}