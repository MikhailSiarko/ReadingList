﻿using System.Collections.Generic;
using ReadingList.Domain.DTO.BookList;

namespace ReadingList.Domain.Queries.SharedList
{
    public class FindSharedListsQuery : IQuery<IEnumerable<SimplifiedSharedBookListDto>>
    {
        public string Query { get; set; }

        public FindSharedListsQuery(string query)
        {
            Query = query;
        }
    }
}
