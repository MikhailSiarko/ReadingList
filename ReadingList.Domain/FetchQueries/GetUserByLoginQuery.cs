namespace ReadingList.Domain.FetchQueries
{
    public class GetUserByLoginQuery
    {
        public readonly string Login;

        public GetUserByLoginQuery(string login)
        {
            Login = login;
        }
    }
}