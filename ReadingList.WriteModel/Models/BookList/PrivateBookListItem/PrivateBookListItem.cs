using System;

namespace ReadingList.WriteModel.Models
{
    public class PrivateBookListItem : BookListItem
    {
        public BookItemStatus Status { get; set; }
        public DateTime LastStatusUpdateDate { get; set; }
        public TimeSpan ReadingTime { get; set; }
    }
}