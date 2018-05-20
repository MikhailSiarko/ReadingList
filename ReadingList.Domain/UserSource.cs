using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ReadingList.Domain.ReadModel;

namespace ReadingList.Domain
{
    public static class UserSource
    {
        private static ConcurrentBag<UserRm> Users { get; } = new ConcurrentBag<UserRm>
        {
            new UserRm
            {
                Id = Guid.NewGuid().ToString(),
                Email = "mike@mail.com",
                Password = "123456",
                Firstname = "Mikhail",
                Lastname = "Siarko"
            },

            new UserRm
            {
                Id = Guid.NewGuid().ToString(),
                Email = "alex@mail.com",
                Password = "654321",
                Firstname = "Alexey",
                Lastname = "Siarko"
            }
        };

        public static ConcurrentBag<UserRm> GetSource() => Users;
    }
}