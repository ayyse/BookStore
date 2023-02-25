using BookStore.Application.GenreOperations.Commands.CreateGenre;
using FluentAssertions;

namespace UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests
    {
        private CreateGenreCommand _command;
        private CreateGenreCommandValidator _validator;
        public CreateGenreCommandValidatorTests()
        {
            _command = new CreateGenreCommand(null, null);
            _validator = new CreateGenreCommandValidator();
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange
            _command.Model = new CreateGenreModel()
            {
                Name = "",
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
            _command.Model = new CreateGenreModel()
            {
                Name = "Mektup"
            };

            // act
            var result = _validator.Validate(_command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
