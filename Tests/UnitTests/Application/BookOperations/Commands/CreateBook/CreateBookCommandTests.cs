using AutoMapper;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.DbOperations;
using BookStore.Entities;
using FluentAssertions;
using UnitTests.TestsSetup;

namespace UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private CreateBookCommand _command;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _command = new CreateBookCommand(_context, _mapper);
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange - hazırlık
            var book = new Book() { 
                Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", 
                PageCount = 334, 
                PublishDate = new DateTime(2013, 04, 01),
                GenreId = 3,
                AuthorId = 3 };

            _context.Books.Add(book);
            _context.SaveChanges();

            _command.Model = new CreateBookModel() { Title = book.Title };

            // act - çalıştırma & assert - doğrulama
            FluentActions
                .Invoking(() => _command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            // arrange 
            CreateBookModel model = new CreateBookModel() { 
                Title = "Test_WhenValidInputsAreGiven_Book_ShouldBeCreated", 
                PageCount = 334, 
                PublishDate = new DateTime(2013, 04, 01).AddYears(-1),
                GenreId = 3,
                AuthorId = 3 };

            _command.Model = model;

            // act 
            FluentActions
                .Invoking(() => _command.Handle()).Invoke();

            // assert
            var book = _context.Books.SingleOrDefault(x => x.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.AuthorId.Should().Be(model.AuthorId);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}
