using System;

namespace ReadingList.ReadModel.Models
{
    public class PrivateBookListItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int Status { get; set; }

        public TimeSpan ReadingTime { get; set; }
    }
}