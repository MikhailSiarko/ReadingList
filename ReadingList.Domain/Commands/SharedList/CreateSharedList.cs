using System;
using System.Collections.Generic;
using System.Linq;
using ReadingList.Domain.Infrastructure;
using ReadingList.Models.Read;
using ReadingList.Models.Write;

namespace ReadingList.Domain.Commands
{
    public class CreateSharedList : SecuredCommand<SharedBookListPreviewDto>
    {
        public readonly string Name;

        public readonly Tag[] Tags;

        public CreateSharedList(int userId, string name, IEnumerable<SelectListItem> tags) : base(userId)
        {
            Name = name;
            Tags = tags.Select(t => new Tag
            {
                Id = Convert.ToInt32(t.Value),
                Name = t.Text
            }).ToArray();
        }
    }
}