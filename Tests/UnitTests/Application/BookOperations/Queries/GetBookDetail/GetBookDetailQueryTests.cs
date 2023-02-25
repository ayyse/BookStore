﻿using AutoMapper;
using BookStore.Application.BookOperations.Queries.GetBookDetail;
using BookStore.DbOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.TestsSetup;

namespace UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        public int BookID { get; set; }

        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _context;
        private GetBookDetailQuery _query;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _query = new GetBookDetailQuery(_context, _mapper);
        }

        [Fact]
        public void WhenThereIsNoBook_InvalidOperationException_ShouldBeReturn()
        {
            _query.BookID = BookID;

            FluentActions
                .Invoking(() => _query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeGetBookDetail()
        {
            _query.BookID = 1;

            // act
            FluentActions.Invoking(() => _query.Handle()).Invoke();

            // assert
            var book = _context.Books.SingleOrDefault(x => x.Id == _query.BookID);
            book.Should().NotBeNull();
        }
    }
}
