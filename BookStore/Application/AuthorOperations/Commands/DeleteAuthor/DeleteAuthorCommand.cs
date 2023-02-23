using BookStore.DbOperations;
using System;
using System.Linq;

namespace BookStore.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorID { get; set; }

        private readonly BookStoreDbContext _context;
        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorID);

            if (author is null)
                throw new InvalidOperationException("Yazar bulunamadı");

            if (author.Book is not null)
                throw new InvalidOperationException("Bu yazara ait bir kitap bulunduğu için yazarı silemezsiniz");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
