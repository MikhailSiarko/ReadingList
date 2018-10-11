using System.Collections.Generic;

namespace ReadingList.ReadModel.Models
{
    public class SharedBookListItemRm : BookListItemRm
    { 
        public string GenreId { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}