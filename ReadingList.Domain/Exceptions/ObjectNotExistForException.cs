using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class ObjectNotExistForException : ObjectStateException
    {
        public ObjectNotExistForException(string entityTypeName, OnExceptionObjectDescriptor objectDescriptor,
            string forTypeName,
            OnExceptionObjectDescriptor forDescriptor)
            : base("{0} " + ExceptionMessages.ObjectNotExist.F(
                       forDescriptor == null
                           ? ExceptionMessages.ForWith.F(forTypeName.ToLower(), GetParams(forDescriptor)).RemoveWith()
                           : ExceptionMessages.ForWith.F(forTypeName.ToLower(), GetParams(forDescriptor))),
                entityTypeName, objectDescriptor)
        {
        }
    }

    public class ObjectNotExistForException<TObject, TFor> : ObjectNotExistForException
    {
        public ObjectNotExistForException(OnExceptionObjectDescriptor objectDescriptor,
            OnExceptionObjectDescriptor forDescriptor)
            : base(typeof(TObject).Name, objectDescriptor, typeof(TFor).Name, forDescriptor)
        {
        }
    }
}