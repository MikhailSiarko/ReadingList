using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class ObjectNotExistForException : ObjectStateException
    {
        public ObjectNotExistForException(string entityTypeName, OnExceptionObjectDescriptor entityInfo, string forTypeName, OnExceptionObjectDescriptor forInfo) 
            : base("{0} " + ExceptionMessages.ObjectNotExist.F(
                       forInfo != null 
                           ? ExceptionMessages.ForWith.F(forTypeName.ToLower(), GetParams(forInfo))
                           : ExceptionMessages.ForWith.F(forTypeName.ToLower(), GetParams(forInfo)).RemoveWith()),
                entityTypeName, entityInfo)
        {
        }
    }
    
    public class ObjectNotExistForException<TObject, TFor> : ObjectStateException
    {
        public ObjectNotExistForException(OnExceptionObjectDescriptor entityInfo, OnExceptionObjectDescriptor forInfo) 
            : base("{0} " + ExceptionMessages.ObjectNotExist.F(
                       forInfo != null 
                           ? ExceptionMessages.ForWith.F(typeof(TFor).Name.TrimModelSuffix().ToLower(), GetParams(forInfo))
                           : ExceptionMessages.ForWith.F(typeof(TFor).Name.TrimModelSuffix().ToLower(), GetParams(forInfo)).RemoveWith()),
                typeof(TObject).Name, entityInfo)
        {
        }
    }
}