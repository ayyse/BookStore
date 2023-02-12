using FluentValidation;

namespace BookStore.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(query => query.BookID).GreaterThan(0);
        }
    }
}
