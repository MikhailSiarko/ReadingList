﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Queries.SharedList;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.QueryHandlers.SharedList
{
    public class FindSharedBookListsQueryHandler : QueryHandler<FindSharedListsQuery, IEnumerable<SimplifiedSharedBookListDto>>
    {
        private readonly IDbReader _dbConnection;
        private readonly ISharedBookListSqlService _sharedBookListSqlService;

        public FindSharedBookListsQueryHandler(IDbReader dbConnection, ISharedBookListSqlService sharedBookListSqlService)
        {
            _dbConnection = dbConnection;
            _sharedBookListSqlService = sharedBookListSqlService;
        }

        protected override async Task<IEnumerable<SimplifiedSharedBookListDto>> Handle(FindSharedListsQuery query)
        {
            var sharedLists =
                await _dbConnection.QueryAsync(_sharedBookListSqlService.GetBookListsSqlQuery(),
                    async reader =>
                    {
                        var lists = (await reader.ReadAsync<SimplifiedSharedBookListRm>()).ToList();

                        var tags = (await reader.ReadAsync<(string TagName, int ListId)>()).ToList();

                        foreach (var listRm in lists)
                        {
                            listRm.Tags = tags.Where(t => t.ListId == listRm.Id).Select(t => t.TagName);
                        }

                        return lists;
                    }) ??
                new List<SimplifiedSharedBookListRm>();

            return Mapper.Map<IEnumerable<SimplifiedSharedBookListRm>, IEnumerable<SimplifiedSharedBookListDto>>(sharedLists);
        }
    }
}