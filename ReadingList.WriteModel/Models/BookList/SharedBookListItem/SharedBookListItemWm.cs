﻿using System.Collections.Generic;
using ReadingList.WriteModel.Models.HelpEntities;

namespace ReadingList.WriteModel.Models
{
    public class SharedBookListItemWm : BookListItemWm
    {
        public IEnumerable<SharedBookListItemTagWm> SharedBookListItemTags { get; set; }
    }
}