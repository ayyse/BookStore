using BookStore.DbOperations;
using System;
using System.Linq;

namespace BookStore.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreID { get; set; }
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreID);
            if (genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı");

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}
