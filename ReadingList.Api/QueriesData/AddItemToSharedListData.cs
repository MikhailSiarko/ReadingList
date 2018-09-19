namespace ReadingList.Api.QueriesData
{
    public class AddItemToSharedListData : AddItemToPrivateListData
    {
        public string GenreId { get; set; }

        public string[] Tags { get; set; }
    }
}