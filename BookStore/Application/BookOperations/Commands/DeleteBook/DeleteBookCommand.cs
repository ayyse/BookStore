using BookStore.DbOperations;
using System;
using System.Linq;

namespace BookStore.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookID { get; set; }
        private readonly IBookStoreDbContext _context;
        public DeleteBookCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == BookID);

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı");

            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}
