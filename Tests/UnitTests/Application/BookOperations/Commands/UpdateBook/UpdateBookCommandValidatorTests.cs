using BookStore.Application.BookOperations.Commands.UpdateBook;
using FluentAssertions;

namespace UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests
    {
        private UpdateBookCommand _command;
        private UpdateBookCommandValidator _validator;
        
        public UpdateBookCommandValidatorTests()
        {
            _command = new UpdateBookCommand(null);
            _validator = new UpdateBookCommandValidator();
        }

        [Theory] // metodu birden fazla şekilde kullanmak için theory kullanılır
        [InlineData("Son Kuşlar", 0, 0, 0)]
        [InlineData("Balıkçının Hikayesi", 0, 1, 0)]
        [InlineData("", 350, 0, 0)]
        [InlineData("", 0, 1, 1)]
        [InlineData("", 410, 1, 2)]
        [InlineData("", 0, 0, 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            // arrange
            _command.Model = new UpdateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId,
                AuthorId = authorId
            };

            // act
            var result = _validator.Validate(_command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            // arrange
            _command.Model = new UpdateBookModel()
            {
                Title = "Balıkçının Hikayesi",
                PageCount = 456,
                PublishDate = DateTime.Now.Date,
                GenreId = 1,
                AuthorId = 1
            };

            // act
            var result = _validator.Validate(_command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            _command.BookID = 2;
            _command.Model = new UpdateBookModel()
            {
                Title = "Balıkçının Hikayesi",
                PageCount = 456,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = 1,
                AuthorId = 1
            };

            // act
            var result = _validator.Validate(_command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
