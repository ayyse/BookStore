using BookStore.Application.BookOperations.Queries.GetBookDetail;
using FluentAssertions;

namespace UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests
    {
        private GetBookDetailQuery _query;
        private GetBookDetailQueryValidator _validator;
        public GetBookDetailQueryValidatorTests()
        {
            _query = new GetBookDetailQuery(null, null);
            _validator = new GetBookDetailQueryValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        [InlineData(-3)]
        public void WhenIdLessThenOrEqualZero_Validator_ShouldBeReturnErrors(int bookId)
        {
            // arrange
            _query.BookID = bookId;

            // act
            var result = _validator.Validate(_query);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(78)]
        public void WhenIdGreaterThenZero_Validator_ShouldNotBeReturnErrors(int bookId)
        {
            // arrange
            _query.BookID = bookId;

            // act
            var result = _validator.Validate(_query);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
