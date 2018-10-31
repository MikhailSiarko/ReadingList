namespace ReadingList.Api.RequestData
{
    public class UpdatePrivateItemRequestData
    {
        public string Author { get; set; }
        
        public string Title { get; set; }

        public int Status { get; set; }
    }
}