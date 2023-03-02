using BookStore.Entities;
using BookStore.TokenOperations.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;

namespace BookStore.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenInstance = new Token();

            // security key in simetriği
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));

            // şifrelenmiş kimlik nesnesi
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // oluşturulacak token ayarları
            tokenInstance.Expiration = DateTime.Now.AddMinutes(15);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: tokenInstance.Expiration,
                notBefore: DateTime.Now, // token üretildikten ne kadar süre sonra devreye girecek
                signingCredentials: signingCredentials
                );

            // token oluşturucu sınıfı
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // token nesnesi
            tokenInstance.AccessToken = tokenHandler.WriteToken(securityToken);

            // refresh token
            tokenInstance.RefreshToken = CreateRefreshToken();
            return tokenInstance;
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
