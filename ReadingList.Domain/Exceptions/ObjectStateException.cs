using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public abstract class ObjectStateException : ApplicationException
    {
        public override string Message { get; }

        protected ObjectStateException(string stateInfo, string entityTypeName,
            OnExceptionObjectDescriptor objectDescriptor)
        {
            var paramsString = GetParams(objectDescriptor);
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
                .TryGetValue($"Object_{objectTypeName}", out value);
        }

        protected static string GetParams(OnExceptionObjectDescriptor objectDescriptor)
        {
            return objectDescriptor == null
                ? string.Empty
                : string.Join(", ", objectDescriptor.Select(x => $"{x.Key}:{x.Value}"));
        }
    }

    public class OnExceptionObjectDescriptor : IEnumerable<KeyValuePair<string, object>>
    {
        private readonly IDictionary<string, object> _dictionary;

        public OnExceptionObjectDescriptor()
        {
            _dictionary = new Dictionary<string, object>();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public object this[string key]
        {
            get => _dictionary[key];

            set => _dictionary.Add(key, value);
        }
    }
}