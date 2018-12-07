using ReadingList.Models.Read;

namespace ReadingList.Domain.Commands
{
    public class UpdatePrivateList : Update<PrivateBookListDto>
    {
        public readonly string Name;

        public UpdatePrivateList(int userId, string name) : base(userId)
        {
            Name = name;
        }
    }
}