using System.Collections.Generic;

namespace ReadingList.Domain.FetchQueries
{
    public class GetExistingTagsQuery
    {
        public IEnumerable<string> TagsNames;

        public GetExistingTagsQuery(IEnumerable<string> tagsNames)
        {
            TagsNames = tagsNames;
        }
    }
}