using System.Collections.Generic;
using ReadingList.Application.DTO.BookList.Abstractions;

namespace ReadingList.Application.DTO.BookList
{
    public class SharedBookListPreviewDto : BookListDto
    {
        public int BooksCount { get; set; }
        
        public IEnumerable<string> Tags { get; set; }
    }
}