using AutoMapper;
using BookStore.DbOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorID { get; set; }
        public AuthorDetailViewModel Model { get; set; }

        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _context;
        public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.Where(x => x.Id == AuthorID).SingleOrDefault();

            if (author is null)
                throw new InvalidOperationException("Yazar bulunamadı");

            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);

            return vm;
        }
    }

    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
    }
}
