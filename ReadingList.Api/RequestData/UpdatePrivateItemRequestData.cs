namespace ReadingList.Api.RequestData
{
    public struct UpdatePrivateItemRequestData
    {
        public string Author { get; set; }
        
        public string Title { get; set; }

        public int Status { get; set; }
    }
}