using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.FetchQueries;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DAO.Identity;
using ReadingList.Domain.Services.Encryption;
using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.CommandHandlers
{
    public class RegisterUserCommandHandler : CommandHandler<RegisterUserCommand>
    {
        private readonly IEncryptionService _encryptionService;

        private readonly IFetchHandler<GetUserByLoginQuery, User> _userFetchHandler;

        public RegisterUserCommandHandler(IDataStorage writeService, IEncryptionService encryptionService,
            IFetchHandler<GetUserByLoginQuery, User> userFetchHandler) : base(writeService)
        {
            _encryptionService = encryptionService;
            _userFetchHandler = userFetchHandler;
        }

        protected override async Task Handle(RegisterUserCommand command)
        {
            var user = await _userFetchHandler.Fetch(new GetUserByLoginQuery(command.Email));

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