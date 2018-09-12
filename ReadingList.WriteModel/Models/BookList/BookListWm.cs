using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ReadingList.WriteModel.Models.Base;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.WriteModel.Models
{
    public class BookListWm : NamedEntityWm
    {
        [IgnoreUpdate]
        public UserWm Owner { get; set; }

        [IgnoreUpdate]
        public int OwnerId { get; set; }

        [IgnoreUpdate]
        public BookListType Type { get; set; }

        public string JsonFields { get; set; }
        
        [IgnoreUpdate]
        public List<SharedBookListTagWm> SharedBookListTags { get; set; }
    }
}