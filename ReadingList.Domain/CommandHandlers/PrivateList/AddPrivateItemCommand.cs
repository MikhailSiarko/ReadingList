using System.Threading.Tasks;
using ReadingList.Domain.Absrtactions;
using ReadingList.Domain.Commands.PrivateList;

namespace ReadingList.Domain.CommandHandlers.PrivateList
{
    public class AddPrivateItemCommandHandler : CommandHandler<AddPrivateItemCommand>
    {
        protected override Task Process(AddPrivateItemCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}