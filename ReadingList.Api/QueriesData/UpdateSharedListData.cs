namespace ReadingList.Api.QueriesData
{
    public class UpdateSharedListData : UpdatePrivateListData
    {
        public string[] Tags { get; set; }

        public string Category { get; set; }
    }
}