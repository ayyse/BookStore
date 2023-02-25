using AutoMapper;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.DbOperations;
using FluentAssertions;
using UnitTests.TestsSetup;

namespace UnitTests.Application.BookOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _context;
        private GetAuthorDetailQuery _query;

        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _query = new GetAuthorDetailQuery(_context, _mapper);
        }

        [Fact]
        public void WhenThereIsNoAuthor_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            _query.AuthorID = 985;

            // act & assert
            FluentActions
                .Invoking(() => _query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Auhtor_ShouldBeGetAuthorDetail()
        {
            // arrange
            _query.AuthorID = 1;

            // act
            FluentActions.Invoking(() => _query.Handle()).Invoke();

            // assert
            var author = _context.Authors.SingleOrDefault(x => x.Id == _query.AuthorID);
            author.Should().NotBeNull();
        }
    }
}
