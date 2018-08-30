using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel.DbConnection;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.QueryHandlers.PrivateList
{
    public class GetPrivateListItemQueryHandler : QueryHandler<GetPrivateListItemQuery, PrivateBookListItemDto>
    {
        private readonly IReadDbConnection _dbConnection;
        private readonly IBookListSqlService _bookListSqlService;

        public GetPrivateListItemQueryHandler(IReadDbConnection dbConnection, IBookListSqlService bookListSqlService)
        {
            _dbConnection = dbConnection;
            _bookListSqlService = bookListSqlService;
        }

        protected override async Task<PrivateBookListItemDto> Handle(GetPrivateListItemQuery query)
        {
            var item = await _dbConnection.QueryFirstAsync<PrivateBookListItemRm>(
                           _bookListSqlService.GetBookListItemSqlQuery(), new
                           {
                               login = query.UserLogin,
                               title = query.Title,
                               author = query.Author
                           }) ??
                       throw new ObjectNotExistException<PrivateBookListItemRm>(new
                       {
                           author = query.Author,
                           title = query.Title
                       });

            return Mapper.Map<PrivateBookListItemRm, PrivateBookListItemDto>(item);
        }
    }
}