using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Read;
using ReadingList.Models.Write;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Domain.CommandHandlers
{
    public class RegisterUserCommandHandler : CommandHandler<RegisterUser, AuthenticationDataDto>
    {
        private readonly IEncryptionService _encryptionService;

        private readonly IFetchHandler<GetUserByLogin, User> _userFetchHandler;

        private readonly IAuthenticationService _authenticationService;

        public RegisterUserCommandHandler(IDataStorage writeService, IEncryptionService encryptionService,
            IFetchHandler<GetUserByLogin, User> userFetchHandler, IAuthenticationService authenticationService) : base(writeService)
        {
            _encryptionService = encryptionService;
            _userFetchHandler = userFetchHandler;
            _authenticationService = authenticationService;
        }

        protected override async Task<AuthenticationDataDto> Handle(RegisterUser command)
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

            if (_encryptionService.Encrypt(command.Password) != user.Password)
                throw new WrongPasswordException();

            return _authenticationService.Authenticate(user);
        }
    }
}