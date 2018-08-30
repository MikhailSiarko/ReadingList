using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class ObjectStateException : ApplicationException
    {
        public override string Message { get; }

        public ObjectStateException(string stateInfo, string entityTypeName, object entityInfo)
        {
            var paramsString = GetParams(entityInfo);
            Message = stateInfo.F(
                TryGetNotExistExceptionMessageByTypeName(entityTypeName, out var message)
                    ? message.F(paramsString)
                    : ExceptionMessages.Object_Default.F(paramsString));
        }
        
        private static bool TryGetNotExistExceptionMessageByTypeName(string objectTypeName, out string value)
        {
            return ExceptionMessages.ResourceManager
                .GetResourceSet(CultureInfo.InvariantCulture, true, true)
                .Cast<DictionaryEntry>()
                .ToDictionary(entry => entry.Key.ToString(), entry => entry.Value.ToString())
                .TryGetValue($"Object_{objectTypeName.TrimModelSuffix()}", out value);
        }
        
        private static string GetParams(object onExceptionMessageParams)
        {
            if(onExceptionMessageParams == null)
                throw new ArgumentNullException(nameof(onExceptionMessageParams));

            return JsonConvert.SerializeObject(onExceptionMessageParams, Formatting.None);
        }
    }
}