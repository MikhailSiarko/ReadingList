using System;
using ReadingList.Resources;

namespace ReadingList.Application.Exceptions
{
    public class WrongPasswordException : ApplicationException
    {
        public WrongPasswordException() : base(ExceptionMessages.WrongPassword)
        {
        }
    }
}