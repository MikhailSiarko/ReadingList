using System;

namespace ReadingList.Api.Queries
{
    public class RegisterQuery : LoginQuery
    {
        public string Id { get; }

        public RegisterQuery()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}