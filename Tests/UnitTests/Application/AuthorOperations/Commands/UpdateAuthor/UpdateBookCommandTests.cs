using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.DbOperations;
using FluentAssertions;
using UnitTests.TestsSetup;

namespace UnitTests.Application.BookOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private UpdateAuthorCommand _command;
        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _command = new UpdateAuthorCommand(_context);
        }

        [Fact]
        public void WhenThereIsNoAuthor_InvalidOperationException_ShouldBeReturn()
        {
            _command.AuthorID = 895;

            FluentActions
                .Invoking(() => _command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            _command.AuthorID = 1;
            
            UpdateAuthorModel model = new UpdateAuthorModel 
            { 
                Name = "Test_WhenValidInputsAreGiven_Author_ShouldBeUpdated",
                BirthDate = new DateTime(2013, 04, 01).AddYears(-1),
            };

            _command.Model = model;

            // act
            FluentActions.Invoking(() => _command.Handle()).Invoke();

            // assert
            var author = _context.Authors.SingleOrDefault(x => x.Id == _command.AuthorID);
            author.Should().NotBeNull();
            author.BirthDate.Should().Be(author.BirthDate);
        }
    }
}
