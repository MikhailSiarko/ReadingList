﻿using System.Threading.Tasks;
using Cinch.SqlBuilder;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Sql.Interfaces;
using ReadingList.ReadModel.DbConnection;
using UserRM = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.QueryHandlers
{
    public class GetUserQueryHandler : QueryHandler<GetUserQuery, UserRM>
    {
        private readonly IReadDbConnection _dbConnection;
        private readonly IUserSqlService _userSqlService;

        public GetUserQueryHandler(IReadDbConnection dbConnection, IUserSqlService userSqlService)
        {
            _dbConnection = dbConnection;
            _userSqlService = userSqlService;
        }

        protected override async Task<UserRM> Handle(GetUserQuery query)
        {
            var user = await _dbConnection.QueryFirstAsync<UserRM>(_userSqlService.GetUserByIdSql(),
                new {id = query.UserId});
            
            if(user == null)
                throw new ObjectNotExistException($"User with Id: {query.UserId}");
            
            return user;
        }
    }
}