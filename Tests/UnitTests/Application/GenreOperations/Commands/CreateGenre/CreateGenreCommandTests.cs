using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.DbOperations;
using BookStore.Entities;
using FluentAssertions;
using UnitTests.TestsSetup;

namespace UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private CreateGenreCommand _command;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _command = new CreateGenreCommand(_context, _mapper);
        }

        [Fact]
        public void WhenAlreadyExistGenreIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange - hazırlık
            var genre = new Genre() 
            { 
                Name = "Test_WhenAlreadyExistGenreIsGiven_InvalidOperationException_ShouldBeReturn"
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();

            _command.Model = new CreateGenreModel() { Name = genre.Name };

            // act - çalıştırma & assert - doğrulama
            FluentActions
                .Invoking(() => _command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            // arrange 
            CreateGenreModel model = new CreateGenreModel() 
            { 
                Name = "Test_WhenValidInputsAreGiven_Book_ShouldBeCreated", 
            };

            _command.Model = model;

            // act 
            FluentActions
                .Invoking(() => _command.Handle()).Invoke();

            // assert
            var genre = _context.Genres.SingleOrDefault(x => x.Name == model.Name);
            genre.Should().NotBeNull();
        }
    }
}
