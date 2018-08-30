using System;
using System.Collections.Generic;

namespace ReadingList.WriteModel.Models
{
    public static class ApplicationRoles
    {
        private static readonly List<RoleWm> Roles;

        static ApplicationRoles()
        {
            Roles = new List<RoleWm>();
            var names = Enum.GetNames(typeof(UserRole));
            foreach (var name in names)
            {
                Roles.Add(new RoleWm { Id = (int)Enum.Parse<UserRole>(name), Name = name});
            }
        }

        public static IEnumerable<RoleWm> GetRoles() => Roles;
    }
}