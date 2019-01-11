using System.Threading.Tasks;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Exceptions;
using ReadingList.Domain.Queries;
using ReadingList.Domain.Services.Interfaces;
using ReadingList.Models.Read;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Domain.CommandHandlers
{
    public class LoginUserCommandHandler : CommandHandler<LoginUser, AuthenticationDataDto>
    {
        private readonly IEncryptionService _encryptionService;

        private readonly IFetchHandler<GetUserByLogin, User> _userFetchHandler;

        private readonly IAuthenticationService _authenticationService;

        public LoginUserCommandHandler(IDataStorage writeService, IEncryptionService encryptionService,
            IFetchHandler<GetUserByLogin, User> userFetchHandler,
            IAuthenticationService authenticationService) : base(writeService)
        {
            _encryptionService = encryptionService;
            _userFetchHandler = userFetchHandler;
            _authenticationService = authenticationService;
        }

        protected override async Task<AuthenticationDataDto> Handle(LoginUser command)
        {
            var user = await _userFetchHandler.Handle(new GetUserByLogin(command.Email)) ??
                       throw new ObjectNotExistException<User>(new OnExceptionObjectDescriptor
                       {
                           ["Login"] = command.Email
                       });

            if (_encryptionService.Encrypt(command.Password) != user.Password)
                throw new WrongPasswordException();

            return _authenticationService.Authenticate(user);
        }
    }
}