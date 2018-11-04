namespace ReadingList.Api.RequestData
{
    public struct SharedItemRequestData
    {
        public string Author { get; set; }

        public string Title { get; set; }

        public string[] Tags { get; set; }
    }
}