using System;
using System.Collections.Generic;
using System.Linq;
using ReadingList.Domain.Infrastructure;
using ReadingList.Models.Read;
using ReadingList.Models.Write;

namespace ReadingList.Domain.Commands
{
    public class UpdateSharedList : Update<SharedBookListPreviewDto>
    {
        public readonly int ListId;

        public readonly string Name;

        public readonly Tag[] Tags;

        public UpdateSharedList(int userId, int listId, string name, IEnumerable<SelectListItem> tags) : base(userId)
        {
            ListId = listId;
            Name = name;
            Tags = tags.Select(t => new Tag
            {
                Id = Convert.ToInt32(t.Value),
                Name = t.Text
            }).ToArray();
        }
    }
}