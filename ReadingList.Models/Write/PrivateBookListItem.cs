using System;

namespace ReadingList.Models.Write
{
    public class PrivateBookListItem : BookListItem
    {
        public BookItemStatus Status { get; set; }

        public DateTimeOffset LastStatusUpdateDate { get; set; }

        public int ReadingTimeInSeconds { get; set; }

        public PrivateBookListItem()
        {
            Status = BookItemStatus.ToReading;
            LastStatusUpdateDate = DateTimeOffset.UtcNow;
        }
    }
}