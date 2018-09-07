using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Infrastructure.Routes;

namespace ReadingList.Api.Infrastructure.Attributes
{
    public class ApiRouteAttribute : RouteAttribute
    {      
        public ApiRouteAttribute(string template) : base(new ApiRouteBuilder().AddSegment(template).ToString())
        {
        }
    }
}