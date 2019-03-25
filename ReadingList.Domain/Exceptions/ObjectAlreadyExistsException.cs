using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class ObjectAlreadyExistsException : ObjectStateException
    {
        protected ObjectAlreadyExistsException(string entityTypeName, OnExceptionObjectDescriptor entityInfo)
            : base(ExceptionMessages.ObjectAlreadyExists, entityTypeName, entityInfo)
        {
        }
    }

    public class ObjectAlreadyExistsException<T> : ObjectAlreadyExistsException
    {
        public ObjectAlreadyExistsException(OnExceptionObjectDescriptor entityInfo) : base(typeof(T).Name, entityInfo)
        {
        }
    }
}