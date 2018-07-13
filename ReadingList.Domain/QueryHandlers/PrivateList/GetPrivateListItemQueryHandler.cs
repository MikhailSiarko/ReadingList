using System.Threading.Tasks;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.Domain.Services.Validation;
using ReadingList.ReadModel.DbConnection;
using ReadingList.WriteModel.Models;
using PrivateListItemRm = ReadingList.ReadModel.Models.PrivateBookListItem;

namespace ReadingList.Domain.QueryHandlers.PrivateList
{
    public class GetPrivateListItemQueryHandler : QueryHandler<GetPrivateListItemQuery, PrivateBookListItemDto>
    {
        private readonly IReadDbConnection _dbConnection;
        private readonly IPrivateBookListSqlService _privateBookListSqlService;

        public GetPrivateListItemQueryHandler(IReadDbConnection dbConnection, IPrivateBookListSqlService privateBookListSqlService)
        {
            _dbConnection = dbConnection;
            _privateBookListSqlService = privateBookListSqlService;
        }

        protected override async Task<PrivateBookListItemDto> Handle(GetPrivateListItemQuery query)
        {
            var item = await _dbConnection.QueryFirstAsync<PrivateListItemRm>(
                _privateBookListSqlService.GetPrivateBookListItemSqlQuery(), new
                {
                    login = query.UserLogin,
                    type = BookListType.Private,
                    title = query.Title,
                    author = query.Author
                });

            EntitiesValidator.Validate(item,
                new OnNotExistExceptionData(typeof(PrivateListItemRm),
                    new {author = query.Author, title = query.Title}));

            return Mapper.Map<PrivateListItemRm, PrivateBookListItemDto>(item);
        }
    }
}