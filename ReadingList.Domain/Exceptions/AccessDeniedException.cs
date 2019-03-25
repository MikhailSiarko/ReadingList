using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class AccessDeniedException : ObjectStateException
    {
        public AccessDeniedException(string objectTypeName, OnExceptionObjectDescriptor objectDescriptor) : base(ExceptionMessages.AccessDenied, objectTypeName, objectDescriptor)
        {
        }
    }

    public class AccessDeniedException<T> : AccessDeniedException
    {
        public AccessDeniedException(OnExceptionObjectDescriptor objectDescriptor) : base(typeof(T).Name, objectDescriptor)
        {
        }
    }
}