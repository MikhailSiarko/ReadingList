namespace ReadingList.Domain.Commands
{
    public class SharePrivateList : SecuredCommand
    {
        public readonly string Name;

        public SharePrivateList(int userId, string name) : base(userId)
        {
            Name = name;
        }
    }
}