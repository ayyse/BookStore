using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using FluentAssertions;

namespace UnitTests.Application.BookOperations.Queries.GetAuthorDetail
{
    public class GetGenreDetailQueryValidatorTests
    {
        private GetAuthorDetailQuery _query;
        private GetAuthorDetailQueryValidator _validator;
        public GetGenreDetailQueryValidatorTests()
        {
            _query = new GetAuthorDetailQuery(null, null);
            _validator = new GetAuthorDetailQueryValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        [InlineData(-3)]
        public void WhenIdLessThenOrEqualZero_Validator_ShouldBeReturnErrors(int authorId)
        {
            // arrange
            _query.AuthorID = authorId;

            // act
            var result = _validator.Validate(_query);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(78)]
        public void WhenIdGreaterThenZero_Validator_ShouldNotBeReturnErrors(int authorId)
        {
            // arrange
            _query.AuthorID = authorId;

            // act
            var result = _validator.Validate(_query);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
