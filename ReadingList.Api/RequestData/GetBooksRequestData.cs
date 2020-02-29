namespace ReadingList.Api.RequestData
{
    public struct GetBooksRequestData
    {
        public string Query { get; set; }

        public int? Chunk { get; set; }

        public int? Count { get; set; }
    }
}