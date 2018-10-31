using System;
using ReadingList.Application.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Application.Exceptions
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