using BookStore.DbOperations;
using System;
using System.Linq;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public UpdateGenreModel Model { get; set; }
        public int GenreID { get; set; }

        private readonly IBookStoreDbContext _context;
        public UpdateGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreID);

            if (genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı");

            genre.Name = Model.Name != default ? Model.Name : genre.Name;

            _context.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
    }
}
