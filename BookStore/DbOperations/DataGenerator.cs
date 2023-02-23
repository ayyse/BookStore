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

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Kardeşimin Hikayesi",
                        GenreId = 3,
                        PageCount = 334,
                        PublishDate = new DateTime(2013, 04, 01),
                        AuthorId = 3
                    },
                    new Book
                    {
                        Title = "Tutunamayanlar",
                        GenreId = 3,
                        PageCount = 671,
                        PublishDate = new DateTime(1971, 01, 01),
                        AuthorId = 2
                    },
                    new Book
                    {
                        Title = "Son Kuşlar",
                        GenreId = 1,
                        PageCount = 350,
                        PublishDate = new DateTime(1952, 07, 05),
                        AuthorId = 1
                    }
                );

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Hikaye"
                    },
                    new Genre
                    {
                        Name = "Kişisel Gelişim"
                    },
                    new Genre
                    {
                        Name = "Roman"
                    },
                    new Genre
                    {
                        Name = "Gezi"
                    },
                    new Genre
                    {
                        Name = "Felsefe"
                    },
                    new Genre
                    {
                        Name = "Deneme"
                    }
                );

                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Sait Faik Abasıyanık",
                        BirthDate = new DateTime(1906, 11, 23)
                    },
                    new Author
                    {
                        Name = "Oğuz Atay",
                        BirthDate = new DateTime(1934, 10, 12)
                    },
                    new Author
                    {
                        Name = "Zülfü Livaneli",
                        BirthDate = new DateTime(1946, 06, 20)
                    },
                    new Author
                    {
                        Name = "Doğan Cüceloğlu",
                        BirthDate = new DateTime(1946, 06, 20)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
