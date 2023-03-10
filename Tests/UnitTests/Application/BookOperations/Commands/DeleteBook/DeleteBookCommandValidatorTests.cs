using BookStore.Application.BookOperations.Commands.DeleteBook;
using FluentAssertions;

namespace UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests
    {
        private DeleteBookCommand _command;
        private DeleteBookCommandValidator _validator;
        public DeleteBookCommandValidatorTests()
        {
            _command = new DeleteBookCommand(null);
            _validator = new DeleteBookCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        [InlineData(-3)]
        public void WhenLessThenOrEqualZeroIdIsGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            // arrange
            _command.BookID = bookId;

            // act
            var result = _validator.Validate(_command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(78)]
        public void WhenGreaterThenZeroIdIsGiven_Validator_ShouldNotBeReturnErrors(int bookId)
        {
            // arrange
            _command.BookID = bookId;

            // act
            var result = _validator.Validate(_command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
