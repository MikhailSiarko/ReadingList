using Microsoft.AspNetCore.Mvc;
using ReadingList.Api.Infrastructure.Routes;
using ReadingList.WriteModel.Models;

namespace ReadingList.Api.Infrastructure.Attributes
{
    public class ListRouteAttribute : RouteAttribute
    {      
        public ListRouteAttribute(BookListType type) 
            : base(new ApiRouteBuilder().AddSegment("list").AddSegment(type.ToString("G").ToLower()).ToString())
        {
        }
    }
}