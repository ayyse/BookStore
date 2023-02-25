using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using FluentAssertions;

namespace UnitTests.Application.BookOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests
    {
        private CreateAuthorCommand _command;
        private CreateAuthorCommandValidator _validator;
        public CreateAuthorCommandValidatorTests()
        {
            _command = new CreateAuthorCommand(null, null);
            _validator = new CreateAuthorCommandValidator();
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange
            _command.Model = new CreateAuthorModel()
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
            _command.Model = new CreateAuthorModel()
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
            _command.Model = new CreateAuthorModel()
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
