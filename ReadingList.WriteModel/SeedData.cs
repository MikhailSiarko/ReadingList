using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.WriteModel.Models;

namespace ReadingList.WriteModel
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<WriteDbContext>();
            context.Database.EnsureCreated();
            InitializeRoles(context);
            InitializeGenres(context);
            context.SaveChanges();
        }

        private static void InitializeRoles(WriteDbContext context)
        {
            var roles = ApplicationRoles.GetRoles();
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(roles);
            }
            else
            {
                var unregisteredRoles = roles.Where(s => !context.Roles.Any(i => i.Id == s.Id && i.Name == s.Name));
                context.Roles.AddRange(unregisteredRoles);
            }
        }
        
        private static void InitializeGenres(WriteDbContext context)
        {
            var genres = GetGenres("genres.json");
            if (!context.Genres.Any())
            {
                context.Genres.AddRange(genres);
            }
            else
            {
                var unregisteredGenres = genres.Where(s => !context.Genres.Any(i => i.Id == s.Id));
                context.Genres.AddRange(unregisteredGenres);
            }
        }

        private static IEnumerable<GenreWm> GetGenres(string genresJson)
        {
            IEnumerable<GenreWm> genres;
            
            var jsonFormatter = new DataContractJsonSerializer(typeof(IEnumerable<GenreWm>));

            using (var fs = new FileStream(Path.Combine(
                Directory.GetParent(Directory.GetCurrentDirectory()).GetDirectories()
                    .Single(d => d.Name == "ReadingList.WriteModel").FullName,
                genresJson), FileMode.Open))
            {
                genres = (IEnumerable<GenreWm>) jsonFormatter.ReadObject(fs);
            }

            return genres;
        }
    }
}
