using System.Collections.Generic;
using ReadingList.WriteModel.Models.Base;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.WriteModel.Models
{
    public class BookWm : EntityWm
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public CategoryWm Category { get; set; }

        public int?  CategoryId { get; set; }

        public List<BookTagWm> BookTags { get; set; }

        public BookWm()
        {
            BookTags = new List<BookTagWm>();
        }
    }
}