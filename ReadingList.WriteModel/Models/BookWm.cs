using System.Collections.Generic;
using ReadingList.WriteModel.Models.Base;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.WriteModel.Models
{
    public class BookWm : EntityWm
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public GenreWm Genre { get; set; }

        public string  GenreId { get; set; }

        public IEnumerable<BookTagWm> BookTags { get; set; }
    }
}