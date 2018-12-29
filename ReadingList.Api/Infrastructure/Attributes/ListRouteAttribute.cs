using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Infrastructure.Routes;

namespace ReadingList.Api.Infrastructure.Attributes
{
    public class ListsRouteAttribute : RouteAttribute
    {
        public ListsRouteAttribute(string type)
            : base(new ApiRouteBuilder("lists").AddSegment(type.ToLower()).ToString())
        {
        }
    }
}