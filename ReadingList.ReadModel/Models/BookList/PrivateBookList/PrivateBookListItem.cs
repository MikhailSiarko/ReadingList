using System;

namespace ReadingList.ReadModel.Models
{
    public class PrivateBookListItem : ReadEntity
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public int Status { get; set; }

        public int ReadingTimeInSeconds { get; set; }
    }
}