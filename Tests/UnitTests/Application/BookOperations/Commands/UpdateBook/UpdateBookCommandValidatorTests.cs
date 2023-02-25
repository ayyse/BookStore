using BookStore.Application.BookOperations.Commands.UpdateBook;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests
    {
        [Theory] // metodu birden fazla şekilde kullanmak için theory kullanılır
        [InlineData("Son Kuşlar", 0, 0, 0)]
        [InlineData("Balıkçının Hikayesi", 0, 1, 0)]
        [InlineData("", 350, 0, 0)]
        [InlineData("", 0, 1, 1)]
        [InlineData("", 410, 1, 2)]
        [InlineData("", 0, 0, 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            // arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId,
                AuthorId = authorId
            };

            // act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookID = 2;
            command.Model = new UpdateBookModel()
            {
                Title = "Balıkçının Hikayesi",
                PageCount = 456,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = 1,
                AuthorId = 1
            };

            // act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
