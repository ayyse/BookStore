using BookStore.DbOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
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
                });
        }
    }
}
