using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.QueryHandlers
{
    public class GetPrivateListItemQueryHandler : QueryHandler<GetPrivateListItemQuery, PrivateBookListItemDto>
    {
        private readonly IDbReader _dbConnection;
        private readonly IBookListSqlService _bookListSqlService;

        public GetPrivateListItemQueryHandler(IDbReader dbConnection, IBookListSqlService bookListSqlService)
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
                               id = query.ItemId
                           }) ??
                       throw new ObjectNotExistException<PrivateBookListItemRm>(new OnExceptionObjectDescriptor
                       {
                           ["Id"] = query.ItemId.ToString()
                       });

            return Mapper.Map<PrivateBookListItemRm, PrivateBookListItemDto>(item);
        }
    }
}