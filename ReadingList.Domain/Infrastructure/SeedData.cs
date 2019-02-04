using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using ReadingList.Models.Write;
using ReadingList.Models.Write.Identity;

namespace ReadingList.Domain.Infrastructure
{
    public static class SeedData
    {
        public static IEnumerable<Genre> Genres()
        {
            IEnumerable<Genre> genres;

            var jsonFormatter = new DataContractJsonSerializer(typeof(IEnumerable<Genre>));

            using (var fs = new FileStream(Path.Combine(
                Directory.GetParent(Directory.GetCurrentDirectory()).GetDirectories()
                    .Single(d => d.Name == "ReadingList.Domain").FullName,
                "Resources\\genres.json"), FileMode.Open))
            {
                genres = (IEnumerable<Genre>) jsonFormatter.ReadObject(fs);
            }

            return genres;
        }

        public static IEnumerable<Role> Roles()
        {
            var names = Enum.GetNames(typeof(UserRole));
            var roles = names.Select(name => new Role {Id = (int) Enum.Parse<UserRole>(name), Name = name}).ToList();
            return roles;
        }
    }
}