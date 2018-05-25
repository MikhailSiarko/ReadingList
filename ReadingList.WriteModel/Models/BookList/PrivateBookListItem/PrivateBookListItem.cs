﻿namespace ReadingList.WriteModel.Models
{
    public class PrivateBookListItem : BookListItem
    {
        public BookItemStatus Status { get; set; }
        public long ReadingTimeInTicks { get; set; }
    }
}