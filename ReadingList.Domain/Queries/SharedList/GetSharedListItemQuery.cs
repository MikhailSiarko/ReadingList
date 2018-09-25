﻿using ReadingList.Domain.DTO.BookList;

namespace ReadingList.Domain.Queries
{
    public class GetSharedListItemQuery : IQuery<SharedBookListItemDto>
    {
        public readonly int ListId;

        public readonly int ItemId;

        public GetSharedListItemQuery(int listId, int itemId)
        {
            ListId = listId;
            ItemId = itemId;
        }
    }
}