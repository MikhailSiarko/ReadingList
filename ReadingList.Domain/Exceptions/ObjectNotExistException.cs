using System;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class ObjectNotExistException : ApplicationException
    {
        public ObjectNotExistException(string objectInfo) : base(ExceptionMessages.ObjectNotExist.F(objectInfo))
        {
        }
    }
}