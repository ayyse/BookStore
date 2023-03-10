using BookStore.DbOperations;
using System;
using System.Linq;

namespace BookStore.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        public int BookID { get; set; }

        private readonly IBookStoreDbContext _context;
        public UpdateBookCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == BookID);

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı");

            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;

            _context.SaveChanges();
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
    }
}
