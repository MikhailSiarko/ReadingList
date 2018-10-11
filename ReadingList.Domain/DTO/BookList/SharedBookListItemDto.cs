﻿using System.Collections.Generic;

namespace ReadingList.Domain.DTO.BookList
{
    public class SharedBookListItemDto
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string GenreId { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
