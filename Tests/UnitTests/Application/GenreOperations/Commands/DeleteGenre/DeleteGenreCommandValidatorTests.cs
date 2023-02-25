using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using FluentAssertions;

namespace UnitTests.Application.BookOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests
    {
        private DeleteGenreCommand _command;
        private DeleteGenreCommandValidator _validator;
        public DeleteGenreCommandValidatorTests()
        {
            _command = new DeleteGenreCommand(null);
            _validator = new DeleteGenreCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        [InlineData(-3)]
        public void WhenLessThenOrEqualZeroIdIsGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
            // arrange
            _command.GenreID = genreId;

            // act
            var result = _validator.Validate(_command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(78)]
        public void WhenGreaterThenZeroIdIsGiven_Validator_ShouldNotBeReturnErrors(int genreId)
        {
            // arrange
            _command.GenreID = genreId;

            // act
            var result = _validator.Validate(_command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
