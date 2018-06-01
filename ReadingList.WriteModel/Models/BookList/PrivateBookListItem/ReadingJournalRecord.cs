using System;
using ReadingList.WriteModel.Models.Base;

namespace ReadingList.WriteModel.Models
{
    public class ReadingJournalRecord : Entity
    {
        public int ItemId { get; set; }
        public PrivateBookListItem Item { get; set; }

        public DateTime StatusChangedDate { get; set; }
        public BookItemStatus StatusSetTo { get; set; }
    }
}
