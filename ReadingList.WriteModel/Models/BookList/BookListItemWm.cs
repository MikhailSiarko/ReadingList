﻿using ReadingList.WriteModel.Models.Base;

namespace ReadingList.WriteModel.Models
{
    public class BookListItemWm : EntityWm
    {
        public string Title { get; set; }

        public string Author { get; set; }
        
        public string GenreId { get; set; }

        public GenreWm Genre { get; set; }

        [IgnoreUpdate]
        public BookListWm BookList { get; set; }

        [IgnoreUpdate]
        public int BookListId { get; set; }
    }
}