using System.Linq;
using System.Threading.Tasks;
using ReadingList.Domain.Queries;
using ReadingList.Domain.ReadModel;

namespace ReadingList.Domain.QueryHandlers
{
    public class GetUserQueryHandler : QueryHandler<GetUserQuery, UserRm>
    {
        protected override Task<UserRm> Process(GetUserQuery command)
        {
            return Task.Run(() => UserSource.GetSource().SingleOrDefault(u => u.Id == command.UserId));
        }
    }
}