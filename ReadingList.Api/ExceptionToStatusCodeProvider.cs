using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ReadingList.Api
{
    public class ExceptionToStatusCodeProvider
    {
        private readonly IReadOnlyDictionary<Type, HttpStatusCode> _source;
        private readonly Type _baseCaseExceptionType;

        public ExceptionToStatusCodeProvider(IReadOnlyDictionary<HttpStatusCode, Type[]> map)
        {
            if(map == null)
                throw new ArgumentNullException(nameof(map));
            
            _baseCaseExceptionType = typeof(Exception);
            _source = InitializeExceptionToStatusCodeMap(map);
        }

        public HttpStatusCode GetStatusCode(Type exceptionType)
        {
            while (exceptionType.IsSubclassOf(_baseCaseExceptionType))
            {
                if (_source.TryGetValue(exceptionType, out var statusCode))
                    return statusCode;
                exceptionType = exceptionType.BaseType;
            }

            return HttpStatusCode.InternalServerError;
        }

        private static IReadOnlyDictionary<Type, HttpStatusCode> InitializeExceptionToStatusCodeMap(IReadOnlyDictionary<HttpStatusCode, Type[]> map)
        {
            return
                map.SelectMany(pair => pair.Value
                        .Select(t => new KeyValuePair<Type, HttpStatusCode>(t, pair.Key)))
                    .Distinct(new KeyValueEqualityComparer())
                    .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        
        private class KeyValueEqualityComparer : IEqualityComparer<KeyValuePair<Type, HttpStatusCode>>
        {
            public bool Equals(KeyValuePair<Type, HttpStatusCode> x, KeyValuePair<Type, HttpStatusCode> y)
            {
                return x.Key == y.Key;
            }

            public int GetHashCode(KeyValuePair<Type, HttpStatusCode> obj)
            {
                return obj.Key.GetHashCode();
            }
        }
    }
}