using System;
using System.Collections.Generic;
using ReadingList.Domain.Enumerations;

namespace ReadingList.Domain.Entities.Identity
{
    public static class ApplicationRoles
    {
        private static readonly List<Role> Roles;

        static ApplicationRoles()
        {
            Roles = new List<Role>();
            var names = Enum.GetNames(typeof(UserRole));
            foreach (var name in names)
            {
                Roles.Add(new Role { Id = (int)Enum.Parse<UserRole>(name), Name = name});
            }
        }

        public static IEnumerable<Role> GetRoles() => Roles;
    }
}