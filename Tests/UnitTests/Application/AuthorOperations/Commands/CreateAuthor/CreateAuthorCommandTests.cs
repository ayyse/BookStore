using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.DbOperations;
using BookStore.Entities;
using FluentAssertions;
using UnitTests.TestsSetup;

namespace UnitTests.Application.BookOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private CreateAuthorCommand _command;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _command = new CreateAuthorCommand(_context, _mapper);
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange - hazırlık
            var author = new Author() 
            { 
                Name = "Test_WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn", 
                BirthDate = new DateTime(2013, 04, 01)
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            _command.Model = new CreateAuthorModel() { Name = author.Name };

            // act - çalıştırma & assert - doğrulama
            FluentActions
                .Invoking(() => _command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            // arrange 
            CreateAuthorModel model = new CreateAuthorModel() 
            { 
                Name = "Test_WhenValidInputsAreGiven_Author_ShouldBeCreated",
                BirthDate = new DateTime(2013, 04, 01).AddYears(-1)
            };

            _command.Model = model;

            // act 
            FluentActions
                .Invoking(() => _command.Handle()).Invoke();

            // assert
            var author = _context.Authors.SingleOrDefault(x => x.Name == model.Name);
            author.Should().NotBeNull();
            author.BirthDate.Should().Be(model.BirthDate);
        }
    }
}
