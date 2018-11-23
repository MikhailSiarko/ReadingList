using System.Collections.Generic;

namespace ReadingList.Domain.Models.DAO
{
    public class Genre
    {
        public Genre()
        {
            Children = new List<Genre>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        public Genre Parent { get; set; }

        public IEnumerable<Genre> Children { get; set; }
    }
}