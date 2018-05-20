using System.Linq;
using System.Threading.Tasks;
using ReadingList.Domain.Queries;
using ReadingList.Domain.ReadModel;

namespace ReadingList.Domain.QueryHandlers
{   
    public class LoginUserQueryHandler : QueryHandler<LoginUserQuery, UserRm>
    {
        protected override Task<UserRm> Process(LoginUserQuery command)
        {
            return Task.Run(() =>
            {
                return UserSource.GetSource()
                    .Single(u => u.Email == command.Email && u.Password == command.Password);
            });
        }
    }
}