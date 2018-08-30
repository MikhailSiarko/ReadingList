using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class ObjectNotExistException : ObjectStateException
    {
        public ObjectNotExistException(string entityTypeName, object entityInfo) 
            : base(ExceptionMessages.ObjectNotExist, entityTypeName, entityInfo)
        {
        } 
    }
    
    public class ObjectNotExistException<T> : ObjectNotExistException
    {
        public ObjectNotExistException(object entityInfo) : base(typeof(T).Name, entityInfo)
        {
        }
    }
}