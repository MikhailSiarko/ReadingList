using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingList.WriteModel.Models
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
    }

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