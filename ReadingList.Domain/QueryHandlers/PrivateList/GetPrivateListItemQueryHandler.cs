using System.Threading.Tasks;
using AutoMapper;
using Cinch.SqlBuilder;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.ReadModel.DbConnection;
using ReadingList.WriteModel.Models;
using PrivateListItemRm = ReadingList.ReadModel.Models.PrivateBookListItem;

namespace ReadingList.Domain.QueryHandlers.PrivateList
{
    public class GetPrivateListItemQueryHandler : QueryHandler<GetPrivateListItemQuery, PrivateBookListItemDto>
    {
        private readonly IReadDbConnection _dbConnection;

        public GetPrivateListItemQueryHandler(IReadDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        protected override async Task<PrivateBookListItemDto> Handle(GetPrivateListItemQuery query)
        {
            var sql = new SqlBuilder()
                .Select("Id", "Title", "Author", "Status", "ReadingTime")
                .From("PrivateBookListItems")
                .Where(
                    "BookListId = (SELECT Id From BookLists WHERE OwnerId = (SELECT Id FROM Users WHERE Login = @login) AND Type = @type) AND Title = @title AND Author = @author")
                .ToSql();
            
            var item = await _dbConnection.QueryFirstAsync<PrivateListItemRm>(sql, new
            {
                login = query.UserLogin,
                type = BookListType.Private,
                title = query.Title,
                author = query.Author
            });
            
            if(item == null)
                throw new ObjectNotFoundException("Item", $"Author: {query.Author} and Title: {query.Title}");

            return Mapper.Map<PrivateListItemRm, PrivateBookListItemDto>(item);
        }
    }
}