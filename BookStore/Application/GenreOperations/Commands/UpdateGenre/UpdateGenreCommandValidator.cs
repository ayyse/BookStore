using FluentValidation;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.GenreID).GreaterThan(0);
            RuleFor(command => command.Model.Name).NotEmpty();
        }
    }
}
