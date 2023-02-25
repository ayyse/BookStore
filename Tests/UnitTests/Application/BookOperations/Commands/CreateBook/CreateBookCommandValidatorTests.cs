using BookStore.Application.BookOperations.Commands.CreateBook;
using FluentAssertions;

namespace UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests
    {
        private CreateBookCommand _command;
        private CreateBookCommandValidator _validator;
        public CreateBookCommandValidatorTests()
        {
            _command = new CreateBookCommand(null, null);
            _validator = new CreateBookCommandValidator();
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
            _command.Model = new CreateBookModel()
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
            _command.Model = new CreateBookModel()
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
            _command.Model = new CreateBookModel()
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
