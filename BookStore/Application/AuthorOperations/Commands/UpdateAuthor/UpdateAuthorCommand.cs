using AutoMapper;
using BookStore.DbOperations;
using System;
using System.Linq;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model { get; set; }
        public int AuthorID { get; set; }

        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorID);

            if (author is null)
                throw new InvalidOperationException("Yazar bulunamadı");

            author.Name = Model.Name != default ? Model.Name : author.Name;
            author.BirthDate = Model.BirthDate != default ? Model.BirthDate : author.BirthDate;
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
