using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.DbOperations;
using FluentAssertions;
using UnitTests.TestsSetup;

namespace UnitTests.Application.BookOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private DeleteGenreCommand _command;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _command = new DeleteGenreCommand(_context);
        }

        [Fact]
        public void WhenThereIsNoGenre_InvalidOperationException_ShouldBeReturn()
        {
            _command.GenreID = 489;

            FluentActions
                .Invoking(() => _command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");
        }

        [Fact]
        public void WhenValidIdIsGiven_Genre_ShouldBeDeleted()
        {
            // arrange 
            _command.GenreID = 3;

            // act 
            FluentActions
                .Invoking(() => _command.Handle()).Invoke();

            // assert
            var genre = _context.Genres.SingleOrDefault(x => x.Id == _command.GenreID);
            genre.Should().BeNull();
        }
    }
}
