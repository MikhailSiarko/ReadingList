using System.Collections.Generic;
using ReadingList.WriteModel.Models.Base;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.WriteModel.Models
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public Category Category { get; set; }
        public int?  CategoryId { get; set; }
        public List<BookTag> BookTags { get; set; }

        public Book()
        {
            BookTags = new List<BookTag>();
        }
    }
}