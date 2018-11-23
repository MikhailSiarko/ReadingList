using System.Collections.Generic;
using ReadingList.Domain.Models.DTO.Abstractions;

namespace ReadingList.Domain.Models.DTO.BookLists
{
    public class SharedBookListPreviewDto : BookListDto
    {
        public int BooksCount { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}