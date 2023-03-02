using AutoMapper;
using BookStore.Application.UserOperations.Commands.CreateUser;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace BookStore.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }

        private readonly IMapper _mapper;
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;
        public CreateTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);

            if (user is not null)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5); // yeni bir access token yaratılana kadar gerekli süre

                _context.SaveChanges();

                return token;
            }
            else
                throw new InvalidOperationException("Kullanıcı adı veya şifre hatalı");

            user = _mapper.Map<User>(Model);

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }

    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
