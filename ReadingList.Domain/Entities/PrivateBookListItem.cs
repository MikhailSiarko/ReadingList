using System;
using ReadingList.Domain.Enumerations;

namespace ReadingList.Domain.Entities
{
    public class PrivateBookListItem : BookListItem
    {
        public BookItemStatus Status { get; set; }

        public DateTime LastStatusUpdateDate { get; set; }

        public int ReadingTimeInSeconds { get; set; }
    }
}