using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace BookStore.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;
        public string RefreshToken;
        public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
        {
            _context = context;

            _configuration = configuration;
        }

        public Token Handle()
        {
            User user = _context.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if (user is not null)
            {

                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();

                return token;
            }
            else
            {
                throw new InvalidOperationException("Valid Refresh Token Bulunamadı");
            }
        }
    }
}
