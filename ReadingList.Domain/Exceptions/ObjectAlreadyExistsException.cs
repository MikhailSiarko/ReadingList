using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class ObjectAlreadyExistsException : ObjectStateException
    {
        public ObjectAlreadyExistsException(string entityTypeName, object entityInfo) 
            : base(ExceptionMessages.ObjectAlreadyExists, entityTypeName, entityInfo)
        {
        }
    }
    
    public class ObjectAlreadyExistsException<T> : ObjectAlreadyExistsException
    {
        public ObjectAlreadyExistsException(object entityInfo) : base(typeof(T).Name, entityInfo)
        {
        }
    }
}