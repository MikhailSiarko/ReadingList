using System;
using System.Collections;
using System.Globalization;
using System.Linq;
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
            
            if(notExistExceptionData == null)
                throw new ArgumentNullException(nameof(notExistExceptionData));
            
            if (TryGetNotExistExceptionMessageByTypeName(notExistExceptionData.EntityTypeName, out var message))
            {
                throw new ObjectNotExistException(message.F(notExistExceptionData.Params));
            }
            
            throw new ObjectNotExistException("Object");
        }
        
        private static bool TryGetNotExistExceptionMessageByTypeName(string objectTypeName, out string value)
        {
            return ExceptionMessages.ResourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true)
                .Cast<DictionaryEntry>()
                .ToDictionary(entry => entry.Key.ToString(), entry => entry.Value.ToString())
                .TryGetValue($"ObjectNotExist_{objectTypeName}", out value);
        }
    }
}