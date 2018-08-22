using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Domain.Commands;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Infrastructure.Converters;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.MapperProfiles;
using ReadingList.ReadModel.DbConnection;
using ReadingList.ReadModel.Models;
using ReadingList.WriteModel;

namespace ReadingList.Domain
{
    public static class DomainModuleExtension
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ICommand).Assembly);
            services.AddDbContext<WriteDbContext>((provider, builder) =>
                builder.UseSqlServer(provider.GetDefaultConnectionString()));
            services.AddScoped<IDbConnection, SqlConnection>(provider =>
                new SqlConnection(provider.GetDefaultConnectionString()));
            services.AddScoped<IReadDbConnection, ReadDbConnection>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerConfigurator.Configure);
            Mapper.Initialize(conf =>
            {
                conf.CreateMap<SharedBookList, SharedBookListDto>().ConvertUsing<SharedBookListConverter>();
                conf.CreateMap<PrivateBookListItem, PrivateBookListItemDto>();
                conf.CreateMap<SharedBookListItem, SharedBookListItemDto>();
                conf.AddProfile<UserProfile>();
            });
            return services;
        }
    }
}