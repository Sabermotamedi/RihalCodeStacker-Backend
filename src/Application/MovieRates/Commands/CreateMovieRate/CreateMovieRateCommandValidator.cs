using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Application.MovieRates.Commands.CreateMovieRate;

namespace Rihal.ReelRise.Application.TodoLists.Commands.CreateTodoList;

public class CreateMovieRateCommandValidator : AbstractValidator<CreateMovieRateCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateMovieRateCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor 
            MovieId
            Rate

        //RuleFor(v => v.MovieId)
        //    .NotEmpty()
        //    .MaximumLength(200)
        //    .MustAsync(BeUniqueTitle)
        //        .WithMessage("'{PropertyName}' must be unique.")
        //        .WithErrorCode("Unique");
    }

    //public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    //{
    //    return await _context.TodoLists
    //        .AllAsync(l => l.Title != title, cancellationToken);
    //}
}
