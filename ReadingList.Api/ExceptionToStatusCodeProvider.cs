using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ReadingList.Api
{
    public class ExceptionToStatusCodeProvider
    {
        private readonly Dictionary<Type, HttpStatusCode> _source;
        private readonly Type _baseCaseExceptionType;

        public ExceptionToStatusCodeProvider(Dictionary<HttpStatusCode, Type[]> map)
        {
            _baseCaseExceptionType = typeof(Exception);
            _source = InitializeExceptionToStatusCodeMap(map);
        }

        public HttpStatusCode GetStatusCode(Type exceptionType)
        {
            while (true)
            {
                if (!exceptionType.IsSubclassOf(_baseCaseExceptionType))
                    return HttpStatusCode.InternalServerError;
                if (_source.TryGetValue(exceptionType, out var statusCode))
                    return statusCode;
                exceptionType = exceptionType.BaseType;
            }
        }

        private static Dictionary<Type, HttpStatusCode> InitializeExceptionToStatusCodeMap(Dictionary<HttpStatusCode, Type[]> map)
        {
            return
                map.SelectMany(pair => pair.Value.Select(t => new KeyValuePair<Type, HttpStatusCode>(t, pair.Key)),
                    (p, c) => c).Distinct(new KeyValueEqualityComparer()).ToDictionary(pair => pair.Key, pair => pair.Value);
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