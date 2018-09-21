using System.Collections.Generic;

namespace ReadingList.WriteModel.Models
{
    public class GenreWm
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        public GenreWm Parent { get; set; }

        public IEnumerable<GenreWm> Children { get; set; }
    }
}