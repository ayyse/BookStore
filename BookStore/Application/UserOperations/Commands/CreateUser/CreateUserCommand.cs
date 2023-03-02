using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;
using System;
using System.Linq;

namespace BookStore.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }

        private readonly IMapper _mapper;
        private readonly IBookStoreDbContext _context;
        public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == Model.Email);

            if (user is not null)
                throw new InvalidOperationException("Kullanıcı zaten mevcut");

            user = _mapper.Map<User>(Model);

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }

    public class CreateUserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
