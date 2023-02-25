using BookStore.DbOperations;
using System;
using System.Linq;

namespace BookStore.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorID { get; set; }

        private readonly IBookStoreDbContext _context;
        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorID);

            if (author is null)
                throw new InvalidOperationException("Yazar bulunamadı");

            //var authorsbook = _context.Books.SingleOrDefault(x => x.AuthorId == AuthorID);

            //if (authorsbook is not null)
            //    throw new InvalidOperationException("Bu yazara ait bir kitap bulunduğu için yazarı silemezsiniz");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
