using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingList.WriteModel.Models
{
    public class PrivateBookListItem : BookListItem
    {
        public BookItemStatus Status { get; set; }

        public DateTime LastStatusUpdateDate { get; set; }

        public int ReadingTimeInSeconds { get; set; }
    }
}