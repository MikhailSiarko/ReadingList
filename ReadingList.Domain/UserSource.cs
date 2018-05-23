using System;
using System.Collections.Concurrent;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain
{
    public static class UserSource
    {
        private static ConcurrentBag<User> Users { get; } = new ConcurrentBag<User>
        {
            new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "mike@mail.com",
                Password = "123456",
                Firstname = "Mikhail",
                Lastname = "Siarko"
            },

            new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "alex@mail.com",
                Password = "654321",
                Firstname = "Alexey",
                Lastname = "Siarko"
            }
        };

        public static ConcurrentBag<User> GetSource() => Users;
    }
}