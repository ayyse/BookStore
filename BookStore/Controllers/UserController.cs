using AutoMapper;
using BookStore.Application.UserOperations.Commands.CreateToken;
using BookStore.Application.UserOperations.Commands.CreateUser;
using BookStore.Application.UserOperations.Commands.RefreshToken;
using BookStore.DbOperations;
using BookStore.TokenOperations.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBookStoreDbContext _context;
        readonly IConfiguration _configuration;
        public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            command.Model = newUser;

            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> Login([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
            command.Model = login;
            var token = command.Handle();
            return token;
        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
            // browser üzerinde kullanıcı logout olmadan oturumun devam etmesi için
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
            command.RefreshToken = token;
            var resultToken = command.Handle();
            return resultToken;
        }
    }
}
