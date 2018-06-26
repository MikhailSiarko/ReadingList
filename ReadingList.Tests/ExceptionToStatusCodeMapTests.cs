using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ReadingList.Api;
using ReadingList.Domain.Exceptions;
using Xunit;

namespace ReadingList.Tests
{
    public class ExceptionToStatusCodeMapTests
    {
        private readonly Dictionary<HttpStatusCode, Type[]> _map;

        public ExceptionToStatusCodeMapTests()
        {
            _map =
                typeof(Startup).GetMethod("InitializeMap", BindingFlags.Static | BindingFlags.NonPublic)
                    .Invoke(new Startup(), null) as Dictionary<HttpStatusCode, Type[]>;
        }

        [Fact]
        public void MapNullReferenceExceptionType()
        {
            var provider = new ExceptionToStatusCodeProvider(_map);
            var statusCode = provider.GetStatusCode(typeof(NullReferenceException));
            Assert.Equal(HttpStatusCode.InternalServerError, statusCode);
        }
        
        [Fact]
        public void MapNotExceptionType()
        {
            var provider = new ExceptionToStatusCodeProvider(_map);
            var statusCode = provider.GetStatusCode(typeof(FactAttribute));
            Assert.Equal(HttpStatusCode.InternalServerError, statusCode);
        }
        
        [Fact]
        public void MapUserWithEmailNotFoundType()
        {
            var provider = new ExceptionToStatusCodeProvider(_map);
            var statusCode = provider.GetStatusCode(typeof(ObjectNotExistException));
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }
        
        [Fact]
        public void MapExceptionType()
        {
            var provider = new ExceptionToStatusCodeProvider(_map);
            var statusCode = provider.GetStatusCode(typeof(Exception));
            Assert.Equal(HttpStatusCode.InternalServerError, statusCode);
        }
    }
}