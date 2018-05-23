using ReadingList.WriteModel.Models.Base;

namespace ReadingList.WriteModel.Models
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }
}