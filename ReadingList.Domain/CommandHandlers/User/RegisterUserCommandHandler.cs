using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.WriteModel;
using ReadingList.WriteModel.Models;
using UserWM = ReadingList.WriteModel.Models.User;

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
            await _context.Users.AddAsync(new UserWM
            {
                Id = command.Id,
                Login = command.Email,
                Password = command.Password,
                Profile = new Profile { Email = command.Email, UserId = command.Id }
            });
            await _context.SaveChangesAsync();
        }
    }
}