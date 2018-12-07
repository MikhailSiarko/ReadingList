using System.Collections.Generic;
using ReadingList.Models.Read.Abstractions;

namespace ReadingList.Models.Read
{
    public class SharedBookListPreviewDto : BookListDto
    {
        public int BooksCount { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}