using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.DbOperations;
using FluentAssertions;
using UnitTests.TestsSetup;

namespace UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
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
            _command.BookID = 489;

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
