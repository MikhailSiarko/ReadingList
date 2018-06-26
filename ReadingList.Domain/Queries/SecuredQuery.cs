namespace ReadingList.Domain.Queries
{
    public abstract class SecuredQuery<T> : IQuery<T>
    {
        public readonly string UserLogin;

        protected SecuredQuery(string userLogin)
        {
            UserLogin = userLogin;
        }
    }
}