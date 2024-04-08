using Rihal.ReelRise.Application.Common.Interfaces;

namespace Rihal.ReelRise.Application.MovieRates.Commands.CreateMovieRate;

public class CreateMovieRateCommandValidator : AbstractValidator<CreateMovieRateCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;


    public CreateMovieRateCommandValidator(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;

        RuleFor(v => v.Rate)
            .LessThanOrEqualTo(10)
            .GreaterThanOrEqualTo(1);

        RuleFor(v => v.MovieId)
            .MustAsync(BeExistInDatabase)
                .WithMessage("This movie is not exist")
                .WithErrorCode("Unique")
            .MustAsync(UserMustBeExist)
                .WithMessage("You have already voted for this movie, you are not allowed to vote again")
                .WithErrorCode("Unique");


    }

    public async Task<bool> BeExistInDatabase(int movieId, CancellationToken cancellationToken)
    {
        return await _context.Movies.AnyAsync(x => x.Id == movieId);
    }

    public async Task<bool> UserMustBeExist(int movieId, CancellationToken cancellationToken)
    {
        var res =  await _context.MovieRates.AnyAsync(x => x.MovieId == movieId && x.UserId == _user.Id);
        return !res;
    }
}
