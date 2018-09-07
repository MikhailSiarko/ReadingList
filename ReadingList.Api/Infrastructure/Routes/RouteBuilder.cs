using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;

namespace ReadingList.Api.Infrastructure.Routes
{
    public class RouteBuilder
    {
        private const char Separator = '/';
        
        private readonly LinkedList<string> _routeSegments;

        public RouteBuilder()
        {
            _routeSegments = new LinkedList<string>();
        }
        
        public RouteBuilder(string rootSegment) : this()
        {
            _routeSegments.AddLast(rootSegment);
        }

        public RouteBuilder AddSegment(string name)
        {
            _routeSegments.AddLast(name);
            return this;
        }

        public override string ToString() => _routeSegments.Join(Separator.ToString());
    }
}