using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadingList.Domain.Entities;
using ReadingList.Domain.Entities.Identity;
using ReadingList.Domain.Enumerations;
using ReadingList.Application.Commands;
using ReadingList.Application.Exceptions;
using ReadingList.Application.Services.Encryption;
using ReadingList.Write;

namespace ReadingList.Application.CommandHandlers
{
    public class RegisterUserCommandHandler : CommandHandler<RegisterUserCommand>
    {
        private readonly IEncryptionService _encryptionService;
        
        public RegisterUserCommandHandler(ApplicationDbContext context, IEncryptionService encryptionService) : base(context)
        {
            _encryptionService = encryptionService;
        }

        protected override async Task Handle(RegisterUserCommand command)
        {
            if(await DbContext.Users.AnyAsync(u => u.Login == command.Email))
                throw new ObjectAlreadyExistsException<User>(new OnExceptionObjectDescriptor
                {
                    ["Email"] = command.Email
                });
            
            var user = new User
            {
                Login = command.Email,
                Password = _encryptionService.Encrypt(command.Password),
                RoleId = (int) UserRole.User,
                Profile = new Profile {Email = command.Email}
            };
            
            await DbContext.Users.AddAsync(user);
            
            await DbContext.BookLists.AddAsync(new BookList
            {
                Name = "Default",
                Owner = user,
                Type = BookListType.Private
            });
            
            await DbContext.SaveChangesAsync();
        }
    }
}