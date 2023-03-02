using BookStore.DbOperations;
using BookStore.Entities;

namespace UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
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
                });
        }
    }
}
