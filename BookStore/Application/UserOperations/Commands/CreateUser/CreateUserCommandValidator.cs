using FluentValidation;

namespace BookStore.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty();
            RuleFor(command => command.Model.Email).NotEmpty();
            RuleFor(command => command.Model.Password).NotEmpty();
        }
    }
}
