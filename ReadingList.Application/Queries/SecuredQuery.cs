namespace ReadingList.Application.Queries
{
    public abstract class SecuredQuery<T> : Query<T>
    {
        public readonly string Login;

        protected SecuredQuery(string login)
        {
            Login = login;
        }
    }
}