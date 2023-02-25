using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.DbOperations;
using FluentAssertions;
using UnitTests.TestsSetup;

namespace UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private UpdateBookCommand _command;
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _command = new UpdateBookCommand(_context);
        }

        [Fact]
        public void WhenThereIsNoBook_InvalidOperationException_ShouldBeReturn()
        {
            _command.BookID = 895;

            FluentActions
                .Invoking(() => _command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            _command.BookID = 1;
            
            UpdateBookModel model = new UpdateBookModel { Title = "Test_WhenValidInputsAreGiven_Book_ShouldBeUpdated", GenreId = 3, PageCount = 334, PublishDate = new DateTime(2013, 04, 01).AddYears(-1), AuthorId = 3 };
            _command.Model = model;

            // act
            FluentActions.Invoking(() => _command.Handle()).Invoke();

            // assert
            var book = _context.Books.SingleOrDefault(x => x.Id == _command.BookID);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(book.PageCount);
            book.PublishDate.Should().Be(book.PublishDate);
            book.Title.Should().Be(model.Title);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
        }
    }
}
