using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.DbOperations;
using FluentAssertions;
using UnitTests.TestsSetup;

namespace UnitTests.Application.BookOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private DeleteAuthorCommand _command;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _command = new DeleteAuthorCommand(_context);
        }

        [Fact]
        public void WhenThereIsNoAuthor_InvalidOperationException_ShouldBeReturn()
        {
            _command.AuthorID = 489;

            FluentActions
                .Invoking(() => _command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı");
        }

        [Fact]
        public void WhenValidIdIsGiven_Author_ShouldBeDeleted()
        {
            // arrange 
            _command.AuthorID = 3;

            // act 
            FluentActions
                .Invoking(() => _command.Handle()).Invoke();

            // assert
            var author = _context.Authors.SingleOrDefault(x => x.Id == _command.AuthorID);
            author.Should().BeNull();
        }
    }
}
