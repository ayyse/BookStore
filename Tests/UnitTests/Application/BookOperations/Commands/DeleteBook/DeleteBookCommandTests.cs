using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.DbOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.TestsSetup;

namespace UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        public int BookID { get; set; }

        private readonly BookStoreDbContext _context;
        private DeleteBookCommand _command;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _command = new DeleteBookCommand(_context);
        }

        [Fact]
        public void WhenThereIsNoBook_InvalidOperationException_ShouldBeReturn()
        {
            _command.BookID = BookID;

            FluentActions
                .Invoking(() => _command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı");
        }

        [Fact]
        public void WhenValidIdIsGiven_Book_ShouldBeDeleted()
        {
            // arrange 
            _command.BookID = 3;

            // act 
            FluentActions
                .Invoking(() => _command.Handle()).Invoke();

            // assert
            var book = _context.Books.SingleOrDefault(x => x.Id == _command.BookID);
            book.Should().BeNull();
        }
    }
}
