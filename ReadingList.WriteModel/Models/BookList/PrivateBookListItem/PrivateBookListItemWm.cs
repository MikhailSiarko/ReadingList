using System;

namespace ReadingList.WriteModel.Models
{
    public class PrivateBookListItemWm : BookListItemWm
    {
        public BookItemStatus Status { get; set; }

        public DateTime LastStatusUpdateDate { get; set; }

        public int ReadingTimeInSeconds { get; set; }
    }
}