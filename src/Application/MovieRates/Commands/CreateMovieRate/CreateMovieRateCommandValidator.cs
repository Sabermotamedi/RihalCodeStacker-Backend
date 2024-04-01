using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Application.MovieRates.Commands.CreateMovieRate;

namespace Rihal.ReelRise.Application.TodoLists.Commands.CreateTodoList;

public class CreateMovieRateCommandValidator : AbstractValidator<CreateMovieRateCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateMovieRateCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Rate)
            .LessThanOrEqualTo(10)
            .GreaterThanOrEqualTo(1)
                .WithMessage("Rate must be in the range of 1 to 10.");

        RuleFor(v => v.MovieId)          
            .MustAsync(BeExistInDatabase)
                .WithMessage("This movie is not exist")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeExistInDatabase(int movieId, CancellationToken cancellationToken)
    {
        return await _context.Movies.AnyAsync(x => x.Id == movieId);
    }
}
