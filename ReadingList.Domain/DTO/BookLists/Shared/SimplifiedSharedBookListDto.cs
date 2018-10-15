using System.Collections.Generic;
using ReadingList.Domain.DTO.BookList.Abstractions;

namespace ReadingList.Domain.DTO.BookList
{
    public class SimplifiedSharedBookListDto : BookListDto
    {
        public int BooksCount { get; set; }
        
        public IEnumerable<string> Tags { get; set; }
    }
}