using AutoMapper;
using BookStore.Common;
using BookStore.DbOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookID { get; set; }
        public BookDetailViewModel Model { get; set; }

        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _context;
        public GetBookDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _context.Books.Include(x => x.Genre).Where(x => x.Id == BookID).SingleOrDefault();

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı");

            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);

            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
