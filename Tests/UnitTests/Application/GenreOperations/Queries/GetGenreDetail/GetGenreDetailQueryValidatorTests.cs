using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using FluentAssertions;

namespace UnitTests.Application.BookOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTests
    {
        private GetGenreDetailQuery _query;
        private GetGenreDetailQueryValidator _validator;
        public GetGenreDetailQueryValidatorTests()
        {
            _query = new GetGenreDetailQuery(null, null);
            _validator = new GetGenreDetailQueryValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        [InlineData(-3)]
        public void WhenIdLessThenOrEqualZero_Validator_ShouldBeReturnErrors(int genreId)
        {
            // arrange
            _query.GenreID = genreId;

            // act
            var result = _validator.Validate(_query);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(78)]
        public void WhenIdGreaterThenZero_Validator_ShouldNotBeReturnErrors(int genreId)
        {
            // arrange
            _query.GenreID = genreId;

            // act
            var result = _validator.Validate(_query);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
