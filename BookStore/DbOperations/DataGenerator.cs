using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookStore.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                    return;

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                );

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Lean Startup",
                        GenreId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 07, 05)
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2011, 10, 05)
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 1,
                        PageCount = 350,
                        PublishDate = new DateTime(2005, 07, 05)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
