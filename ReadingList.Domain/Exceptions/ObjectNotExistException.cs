using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class ObjectNotExistException : ApplicationException
    {
        public override string Message { get; }

        protected ObjectNotExistException(string entityTypeName, object entityInfo)
        {
            var paramsString = GetParams(entityInfo);
            Message = ExceptionMessages.ObjectNotExist.F(
                TryGetNotExistExceptionMessageByTypeName(entityTypeName, out var message)
                    ? message.F(paramsString)
                    : ExceptionMessages.ObjectNotExist_Default.F(paramsString));
        }
        
        private static bool TryGetNotExistExceptionMessageByTypeName(string objectTypeName, out string value)
        {
            return ExceptionMessages.ResourceManager
                .GetResourceSet(CultureInfo.InvariantCulture, true, true)
                .Cast<DictionaryEntry>()
                .ToDictionary(entry => entry.Key.ToString(), entry => entry.Value.ToString())
                .TryGetValue($"ObjectNotExist_{objectTypeName}", out value);
        }
        
        private static string GetParams(object onExceptionMessageParams)
        {
            if(onExceptionMessageParams == null)
                throw new ArgumentNullException(nameof(onExceptionMessageParams));

            return JsonConvert.SerializeObject(onExceptionMessageParams, Formatting.None);
        }
    }
    
    public class ObjectNotExistException<T> : ObjectNotExistException
    {
        public ObjectNotExistException(object entityInfo) : base(typeof(T).Name, entityInfo)
        {
        }
    }
}