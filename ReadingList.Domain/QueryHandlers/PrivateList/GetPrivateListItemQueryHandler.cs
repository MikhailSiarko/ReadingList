using System;
using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel.DbConnection;
using ReadingList.WriteModel.Models;
using PrivateListItemRm = ReadingList.ReadModel.Models.PrivateBookListItem;

namespace ReadingList.Domain.QueryHandlers.PrivateList
{
    public class GetPrivateListItemQueryHandler : QueryHandler<GetPrivateListItemQuery, PrivateBookListItemDto>
    {
        private readonly IReadDbConnection _dbConnection;
        private readonly IBookListSqlService _bookListSqlService;

        public GetPrivateListItemQueryHandler(IReadDbConnection dbConnection, Func<BookListType, IBookListSqlService> sqlServiceAccessor)
        {
            _dbConnection = dbConnection;
            _bookListSqlService = sqlServiceAccessor(BookListType.Private);
        }

        protected override async Task<PrivateBookListItemDto> Handle(GetPrivateListItemQuery query)
        {
            var item = await _dbConnection.QueryFirstAsync<PrivateListItemRm>(
                           _bookListSqlService.GetBookListItemSqlQuery(), new
                           {
                               login = query.UserLogin,
                               title = query.Title,
                               author = query.Author
                           }) ??
                       throw new ObjectNotExistException<PrivateListItemRm>(new
                       {
                           author = query.Author,
                           title = query.Title
                       });

            return Mapper.Map<PrivateListItemRm, PrivateBookListItemDto>(item);
        }
    }
}