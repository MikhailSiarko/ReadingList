using System.Collections.Generic;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.WriteModel.Models
{
    public class SharedBookListItemWm : BookListItemWm
    {
        public List<SharedBookListItemTagWm> SharedBookListItemTags { get; set; }

        public GenreWm Genre { get; set; }

        public string GenreId { get; set; }

        public SharedBookListItemWm()
        {
            SharedBookListItemTags = new List<SharedBookListItemTagWm>();
        }
    }
}