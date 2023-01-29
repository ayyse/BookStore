using BookStore.Common;
using BookStore.DbOperations;
using System;
using System.Linq;

namespace BookStore.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookID { get; set; }
        private readonly BookStoreDbContext _context;
        public GetBookDetailQuery(BookStoreDbContext context)
        {
            _context = context;
        }

        public BookDetailViewModel Handle()
        {
            var book = _context.Books.Where(x => x.Id == BookID).SingleOrDefault();

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı");

            BookDetailViewModel vm = new BookDetailViewModel();
            vm.Title = book.Title;
            vm.GenreId = ((GenreEnum)book.GenreId).ToString();
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");

            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string GenreId { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
