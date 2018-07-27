using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using ReadingList.Api;
using ReadingList.Domain.Exceptions;
using Xunit;

namespace ReadingList.Tests
{
    public class ExceptionToStatusCodeMappingTests
    {
        private readonly Dictionary<HttpStatusCode, Type[]> _map;

        public ExceptionToStatusCodeMappingTests()
        {
            _map =
                typeof(Startup).GetMethod("InitializeMap", BindingFlags.Static | BindingFlags.NonPublic)
                    .Invoke(new Startup(), null) as Dictionary<HttpStatusCode, Type[]>;
        }

        [Fact]
        public void GetStatusCode_ReturnsInternalServerError_When_TypeIsNullReferenceException()
        {
            var provider = new ExceptionToStatusCodeProvider(_map);
            var statusCode = provider.GetStatusCode(typeof(NullReferenceException));
            Assert.Equal(HttpStatusCode.InternalServerError, statusCode);
        }
        
        [Fact]
        public void GetStatusCode_ReturnsInternalServerError_When_TypeIsFactAttribute()
        {
            var provider = new ExceptionToStatusCodeProvider(_map);
            var statusCode = provider.GetStatusCode(typeof(FactAttribute));
            Assert.Equal(HttpStatusCode.InternalServerError, statusCode);
        }
        
        [Fact]
        public void GetStatusCode_ReturnsBadRequest_When_TypeIsObjectNotExistException()
        {
            var provider = new ExceptionToStatusCodeProvider(_map);
            var statusCode = provider.GetStatusCode(typeof(ObjectNotExistException));
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }
        
        [Fact]
        public void GetStatusCode_ReturnsInternalServerError_When_TypeIsException()
        {
            var provider = new ExceptionToStatusCodeProvider(_map);
            var statusCode = provider.GetStatusCode(typeof(Exception));
            Assert.Equal(HttpStatusCode.InternalServerError, statusCode);
        }
    }
}