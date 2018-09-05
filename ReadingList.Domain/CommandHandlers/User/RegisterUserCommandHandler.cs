using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Services.Encryption;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.CommandHandlers
{
    public class RegisterUserCommandHandler : CommandHandler<RegisterUserCommand>
    {
        private readonly WriteDbContext _context;
        private readonly IEncryptionService _encryptionService;
        public RegisterUserCommandHandler(WriteDbContext context, IEncryptionService encryptionService)
        {
            _context = context;
            _encryptionService = encryptionService;
        }

        protected override async Task Handle(RegisterUserCommand command)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == command.Email);
            
            if(user != null)
                throw new ObjectAlreadyExistsException<UserWm>(new OnExceptionObjectDescriptor
                {
                    ["Email"] = command.Email
                });
            
            var userRm = new UserWm
            {
                Login = command.Email,
                Password = _encryptionService.Encrypt(command.Password),
                RoleId = (int) UserRole.User,
                Profile = new ProfileWm {Email = command.Email}
            };
            
            await _context.Users.AddAsync(userRm);
            
            await _context.BookLists.AddAsync(new BookListWm
            {
                Name = "Default",
                Owner = userRm,
                Type = BookListType.Private
            });
            
            await _context.SaveChangesAsync();
        }
    }
}