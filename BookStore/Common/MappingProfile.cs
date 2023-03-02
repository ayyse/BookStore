using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.Application.AuthorOperations.Queries.GetAuthors;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.Application.BookOperations.Queries.GetBookDetail;
using BookStore.Application.BookOperations.Queries.GetBooks;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.Application.UserOperations.Commands.CreateUser;
using BookStore.Entities;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name));
            CreateMap<Book, BooksViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<CreateAuthorModel, Author>();

            CreateMap<CreateUserModel, User>();
        }
    }
}
