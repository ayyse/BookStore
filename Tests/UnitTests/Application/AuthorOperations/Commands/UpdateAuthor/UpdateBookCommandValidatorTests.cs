using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using FluentAssertions;

namespace UnitTests.Application.BookOperations.Commands.UpdateAuthor
{
    public class UpdateGenreCommandValidatorTests
    {
        private UpdateAuthorCommand _command;
        private UpdateAuthorCommandValidator _validator;
        
        public UpdateGenreCommandValidatorTests()
        {
            _command = new UpdateAuthorCommand(null);
            _validator = new UpdateAuthorCommandValidator();
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange
            _command.Model = new UpdateAuthorModel()
            {
                Name = "",
                BirthDate = DateTime.Now.Date.AddYears(-1)
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
            _command.Model = new UpdateAuthorModel()
            {
                Name = "Ahmet Ümit",
                BirthDate = DateTime.Now.Date
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
            _command.AuthorID = 2;
            _command.Model = new UpdateAuthorModel()
            {
                Name = "Ahmet Ümit",
                BirthDate = DateTime.Now.Date.AddYears(-1)
            };

            // act
            var result = _validator.Validate(_command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
