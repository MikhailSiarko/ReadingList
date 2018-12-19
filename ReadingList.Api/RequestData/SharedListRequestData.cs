using ReadingList.Domain.Infrastructure;

namespace ReadingList.Api.RequestData
{
    public struct SharedListRequestData
    {
        public string Name { get; set; }

        public SelectListItem[] Tags { get; set; }
    }
}