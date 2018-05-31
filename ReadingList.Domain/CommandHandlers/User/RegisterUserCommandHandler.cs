using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Services.Encryption;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;
using UserWm = ReadingList.WriteModel.Models.User;
using ProfileWm = ReadingList.WriteModel.Models.Profile;

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
            var userRm = new UserWm
            {
                Login = command.Email,
                Password = _encryptionService.Encrypt(command.Password),
                RoleId = (int) UserRole.User,
                Profile = new ProfileWm {Email = command.Email}
            };
            await _context.Users.AddAsync(userRm);
            await _context.BookLists.AddAsync(new BookList
            {
                Name = "Default",
                Owner = userRm,
                Type = BookListType.Private
            });
            await _context.SaveChangesAsync();
        }
    }
}