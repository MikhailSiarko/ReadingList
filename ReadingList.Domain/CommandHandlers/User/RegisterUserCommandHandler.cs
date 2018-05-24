using System.Threading.Tasks;
using ReadingList.Domain.Absrtactions;
using ReadingList.Domain.Commands;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;
using UserWm = ReadingList.WriteModel.Models.User;
using ProfileWm = ReadingList.WriteModel.Models.Profile;

namespace ReadingList.Domain.CommandHandlers
{
    public class RegisterUserCommandHandler : CommandHandler<RegisterUserCommand>
    {
        private readonly ReadingListDbContext _context;
        public RegisterUserCommandHandler(ReadingListDbContext context)
        {
            _context = context;
        }

        protected override async Task Process(RegisterUserCommand command)
        {
            await _context.Users.AddAsync(new UserWm
            {
                Login = command.Email,
                Password = command.Password,
                RoleId = (int)UserRole.User,
                Profile = new ProfileWm { Email = command.Email }
            });
            await _context.SaveChangesAsync();
        }
    }
}