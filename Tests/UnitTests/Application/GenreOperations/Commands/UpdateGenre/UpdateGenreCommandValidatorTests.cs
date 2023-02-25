using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using FluentAssertions;

namespace UnitTests.Application.BookOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests
    {
        private UpdateGenreCommand _command;
        private UpdateGenreCommandValidator _validator;
        
        public UpdateGenreCommandValidatorTests()
        {
            _command = new UpdateGenreCommand(null);
            _validator = new UpdateGenreCommandValidator();
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange
            _command.Model = new UpdateGenreModel()
            {
                Name = ""
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
            _command.GenreID = 2;
            _command.Model = new UpdateGenreModel()
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
