namespace ReadingList.Api.QueriesData
{
    public class UpdateSharedListItemData : AddItemToPrivateListData
    {
        public string GenreId { get; set; }

        public string[] Tags { get; set; }
    }
}
