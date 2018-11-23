namespace ReadingList.Api.Infrastructure.Routes
{
    public class ApiRouteBuilder : RouteBuilder
    {
        public ApiRouteBuilder() : base("api")
        {
        }

        public ApiRouteBuilder(string segment) : base("api")
        {
            AddSegment(segment);
        }
    }
}