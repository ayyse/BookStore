using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.DbOperations;
using FluentAssertions;
using UnitTests.TestsSetup;

namespace UnitTests.Application.BookOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _context;
        private GetGenreDetailQuery _query;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _query = new GetGenreDetailQuery(_context, _mapper);
        }

        [Fact]
        public void WhenThereIsNoGenre_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            _query.GenreID = 77;

            // act & assert
            FluentActions
                .Invoking(() => _query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeGetGenreDetail()
        {
            // arrange
            _query.GenreID = 1;

            // act
            FluentActions.Invoking(() => _query.Handle()).Invoke();

            // assert
            var genre = _context.Genres.SingleOrDefault(x => x.Id == _query.GenreID);
            genre.Should().NotBeNull();
        }
    }
}
