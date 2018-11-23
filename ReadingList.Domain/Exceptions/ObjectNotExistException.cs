using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class ObjectNotExistException : ObjectStateException
    {
        public ObjectNotExistException(string entityTypeName, OnExceptionObjectDescriptor objectDescriptor)
            : base(ExceptionMessages.ObjectNotExist, entityTypeName, objectDescriptor)
        {
        }
    }

    public class ObjectNotExistException<T> : ObjectNotExistException
    {
        public ObjectNotExistException(OnExceptionObjectDescriptor objectDescriptor) : base(typeof(T).Name,
            objectDescriptor)
        {
        }
    }
}