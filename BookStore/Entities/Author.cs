using System;
using System.Collections.Generic;

namespace BookStore.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        public List<Book> Book { get; set; } // Bir yazarın birden fazla kitabı olabilir
    }
}
