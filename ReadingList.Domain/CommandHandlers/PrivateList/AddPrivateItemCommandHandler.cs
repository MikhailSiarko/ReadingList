﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands.PrivateList;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;
using PrivateBookListItemWm = ReadingList.WriteModel.Models.PrivateBookListItem;

namespace ReadingList.Domain.CommandHandlers.PrivateList
{
    public class AddPrivateItemCommandHandler : CommandHandler<AddPrivateItemCommand>
    {
        private readonly WriteDbContext _dbContext;

        public AddPrivateItemCommandHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(AddPrivateItemCommand command)
        {
            var userId = await _dbContext.Users.AsNoTracking().Where(u => u.Login == command.Login).Select(u => u.Id)
                .SingleAsync();
            var listId = await _dbContext.BookLists.AsNoTracking()
                .Where(l => l.OwnerId == userId && l.Type == BookListType.Private).Select(l => l.Id).SingleAsync();
            var listItem = new PrivateBookListItemWm
            {
                Status = BookItemStatus.ToReading,
                BookListId = listId,
                Title = command.Title,
                Author = command.Author
            };
            await _dbContext.PrivateBookListItems.AddAsync(listItem);
            await _dbContext.ReadingJournalRecords.AddAsync(new ReadingJournalRecord
            {
                StatusChangedDate = DateTime.Now,
                StatusSetTo = BookItemStatus.ToReading,
                Item = listItem
            });            
            await _dbContext.SaveChangesAsync();
        }
    }
}