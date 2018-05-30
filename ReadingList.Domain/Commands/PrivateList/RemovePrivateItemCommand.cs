using ReadingList.Domain.Abstractions;

namespace ReadingList.Domain.Commands.PrivateList
{
    public class RemovePrivateItemCommand : ICommand
    {
        public readonly int Id;

        public RemovePrivateItemCommand(int id)
        {
            Id = id;
        }
    }
}