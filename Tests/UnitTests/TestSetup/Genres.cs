using BookStore.DbOperations;
using BookStore.Entities;

namespace UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
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
                });
        }
    }
}
