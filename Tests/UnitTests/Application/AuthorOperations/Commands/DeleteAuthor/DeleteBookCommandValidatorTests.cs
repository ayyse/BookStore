using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using FluentAssertions;

namespace UnitTests.Application.BookOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests
    {
        private DeleteAuthorCommand _command;
        private DeleteAuthorCommandValidator _validator;
        public DeleteAuthorCommandValidatorTests()
        {
            _command = new DeleteAuthorCommand(null);
            _validator = new DeleteAuthorCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        [InlineData(-3)]
        public void WhenLessThenOrEqualZeroIdIsGiven_Validator_ShouldBeReturnErrors(int authorId)
        {
            // arrange
            _command.AuthorID = authorId;

            // act
            var result = _validator.Validate(_command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(78)]
        public void WhenGreaterThenZeroIdIsGiven_Validator_ShouldNotBeReturnErrors(int authorId)
        {
            // arrange
            _command.AuthorID = authorId;

            // act
            var result = _validator.Validate(_command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
