using AutoMapper;
using BookStore.DbOperations;
using System;
using System.Linq;

namespace BookStore.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreID { get; set; }
        public GenreDetailViewModel Model { get; set; }

        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _context;
        public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.Where(x => x.IsActive && x.Id == GenreID).SingleOrDefault();

            if (genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı");

            GenreDetailViewModel vm = _mapper.Map<GenreDetailViewModel>(genre);

            return vm;
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
