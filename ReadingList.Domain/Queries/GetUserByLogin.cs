namespace ReadingList.Domain.Queries
{
    public class GetUserByLogin
    {
        public readonly string Login;

        public GetUserByLogin(string login)
        {
            Login = login;
        }
    }
}