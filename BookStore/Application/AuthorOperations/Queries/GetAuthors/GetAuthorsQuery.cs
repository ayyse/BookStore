using AutoMapper;
using BookStore.DbOperations;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IMapper _mapper;
        private readonly IBookStoreDbContext _context;
        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authorList = _context.Authors.OrderBy(x => x.Id);
            List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authorList);
            return vm;
        }
    }

    public class AuthorsViewModel
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
    }
}
