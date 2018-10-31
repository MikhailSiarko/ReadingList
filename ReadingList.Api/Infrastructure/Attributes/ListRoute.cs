using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Infrastructure.Routes;

namespace ReadingList.Api.Infrastructure.Attributes
{
    public class ListRouteAttribute : RouteAttribute
    {      
        public ListRouteAttribute(string type) 
            : base(new ApiRouteBuilder("list").AddSegment(type.ToLower()).ToString())
        {
        }
    }
}