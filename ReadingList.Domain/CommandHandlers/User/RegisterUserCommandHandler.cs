using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Write;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Domain.CommandHandlers
{
    public class RegisterUserCommandHandler : CommandHandler<RegisterUser>
    {
        private readonly IEncryptionService _encryptionService;

        private readonly IFetchHandler<GetUserByLogin, User> _userFetchHandler;

        public RegisterUserCommandHandler(IDataStorage writeService, IEncryptionService encryptionService,
            IFetchHandler<GetUserByLogin, User> userFetchHandler) : base(writeService)
        {
            _encryptionService = encryptionService;
            _userFetchHandler = userFetchHandler;
        }

        protected override async Task Handle(RegisterUser command)
        {
            var user = await _userFetchHandler.Handle(new GetUserByLogin(command.Email));

            if (user != null)
                throw new ObjectAlreadyExistsException<User>(new OnExceptionObjectDescriptor
                {
                    ["Email"] = command.Email
                });

            user = new User
            {
                Login = command.Email,
                Password = _encryptionService.Encrypt(command.Password),
                RoleId = (int) UserRole.User,
                Profile = new Profile {Email = command.Email}
            };

            await WriteService.SaveAsync(user);

            await WriteService.SaveAsync(new BookList
            {
                Name = "Default",
                OwnerId = user.Id,
                Type = BookListType.Private
            });
        }
    }
}