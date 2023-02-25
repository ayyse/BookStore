using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.DbOperations;
using FluentAssertions;
using UnitTests.TestsSetup;

namespace UnitTests.Application.BookOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private UpdateGenreCommand _command;
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _command = new UpdateGenreCommand(_context);
        }

        [Fact]
        public void WhenThereIsNoGenre_InvalidOperationException_ShouldBeReturn()
        {
            _command.GenreID = 895;

            FluentActions
                .Invoking(() => _command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            _command.GenreID = 1;

            UpdateGenreModel model = new UpdateGenreModel
            {
                Name = "Test_WhenValidInputsAreGiven_Genre_ShouldBeUpdated"
            };
            _command.Model = model;

            // act
            FluentActions.Invoking(() => _command.Handle()).Invoke();

            // assert
            var genre = _context.Genres.SingleOrDefault(x => x.Id == _command.GenreID);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(genre.Name);
        }
    }
}
