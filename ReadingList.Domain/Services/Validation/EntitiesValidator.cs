using System;
using System.Linq;
using System.Reflection;
using System.Text;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Domain.Services.Validation
{
    public static class EntitiesValidator
    {
        public static void Validate(object entity, OnNotExistExceptionData notExistExceptionData)
        {
            if (entity != null) return;
            if (ExceptionMessages.ResourceManager.TryGetNotExistExceptionMessageByTypeName(notExistExceptionData.EntityType, out var message))
            {
                throw new ObjectNotExistException(message.F(notExistExceptionData.Params));
            }
            throw new ObjectNotExistException("Object");
        }
    }

    public class OnNotExistExceptionData
    {
        public readonly Type EntityType;

        public string Params
        {
            get
            {
                var stringBuilder = _messageParams.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Select(prop => $"{prop.Name}: {prop.GetValue(_messageParams)}").Aggregate(new StringBuilder(),
                        (builder, s) => builder.AppendFormat("{0}, ", s));

                return stringBuilder.ToString(0, stringBuilder.Length - 2);
            }
        }

        private readonly object _messageParams;

        public OnNotExistExceptionData(Type entityType, object messageParams)
        {
            EntityType = entityType;
            _messageParams = messageParams;
        }
    }
}