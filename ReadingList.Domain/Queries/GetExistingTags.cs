using System.Collections.Generic;

namespace ReadingList.Domain.Queries
{
    public class GetExistingTags
    {
        public readonly IEnumerable<string> TagsNames;

        public GetExistingTags(IEnumerable<string> tagsNames)
        {
            TagsNames = tagsNames;
        }
    }
}