using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.ReadModel;

namespace ReadingList.Domain.CommandHandlers
{
    public class RegisterUserCommandHandler : CommandHandler<RegisterUserCommand>
    {
        protected override Task Process(RegisterUserCommand command)
        {
            return Task.Run(() => UserSource.GetSource().Add(new UserRm
            {
                Email = command.Email,
                Id = command.Id,
                Password = command.Password
            }));
        }
    }
}