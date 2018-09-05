using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public abstract class ObjectStateException : ApplicationException
    {
        public override string Message { get; }
        
        // TODO Message is building from the end. Need to refactor for building message from the beginning
        protected ObjectStateException(string stateInfo, string entityTypeName, OnExceptionObjectDescriptor entityInfo)
        {
            var paramsString = GetParams(entityInfo);
            Message = stateInfo.F(
                TryGetNotExistExceptionMessageByTypeName(entityTypeName, out var message)
                    ? !string.IsNullOrEmpty(paramsString) ? message.F(paramsString) : message.RemoveWithData()
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
        
        protected static string GetParams(OnExceptionObjectDescriptor onExceptionMessageParams)
        {
            return onExceptionMessageParams == null
                ? string.Empty
                : onExceptionMessageParams.Select(x => $"{x.Key}:{x.Value}").Join();
        }
    }

    public class OnExceptionObjectDescriptor : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly IDictionary<string, string> _dictionary;

        public OnExceptionObjectDescriptor()
        {
            _dictionary = new Dictionary<string, string>();
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string this[string key]
        {
            get => _dictionary[key];

            set => _dictionary.Add(key, value);
        }
    }
}