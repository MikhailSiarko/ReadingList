using System;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class ObjectNotFoundException : ApplicationException
    {
        public ObjectNotFoundException(string kindOfObject, string objectData) 
            : base(ExceptionMessages.ObjectNotFoundWithData.F(kindOfObject, objectData))
        {
        }
        
        public ObjectNotFoundException(string kindOfObject) 
            : base(ExceptionMessages.ObjectNotFound.F(kindOfObject))
        {
        }
    }
}