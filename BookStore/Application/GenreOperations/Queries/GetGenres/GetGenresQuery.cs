using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IMapper _mapper;
        private readonly IBookStoreDbContext _context;
        public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genreList = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
            List<GenresViewModel> vm = _mapper.Map<List<GenresViewModel>>(genreList);
            return vm;
        }
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
