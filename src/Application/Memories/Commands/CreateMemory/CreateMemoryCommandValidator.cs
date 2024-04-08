using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Application.MovieRates.Commands.CreateMemory;

namespace Rihal.ReelRise.Application.Memories.Commands.CreateMemory;

public class CreateMemoryCommandValidator : AbstractValidator<CreateMemoryCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;


    public CreateMemoryCommandValidator(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;

        RuleFor(v => v.Title)           
            .NotEmpty();

        RuleFor(v => v.Story)
          .NotEmpty();

        RuleFor(v => v.SawDate)
        .NotEmpty();

        RuleFor(v => v)
            .MustAsync(async (command, cancellationToken) =>
                await BeGreaterThanReleaseDate(command.MovieId, command.SawDate, cancellationToken))
            .WithMessage("Saw date should be greater than release date.")
            .WithErrorCode("Unique");

        RuleFor(v => v.MovieId)
            .MustAsync(BeExistInDatabase)
                .WithMessage("This movie is not exist.")
                .WithErrorCode("Unique")
            .MustAsync(IsUnique)
                .WithMessage("You have already commit story for this movie, you are not allowed to commit again.")
                .WithErrorCode("Unique");


    }

    public async Task<bool> BeExistInDatabase(int movieId, CancellationToken cancellationToken)
    {
        return await _context.Movies.AnyAsync(x => x.Id == movieId);
    }

    public async Task<bool> IsUnique(int movieId, CancellationToken cancellationToken)
    {
        var res =  await _context.Memories.AnyAsync(x => x.MovieId == movieId && x.UserId == _user.Id);
        return !res;
    }

    public async Task<bool> BeGreaterThanReleaseDate(int movieId, DateTime sawDate, CancellationToken cancellationToken)
    {
        var res = await _context.Movies.AnyAsync(x => x.Id == movieId && x.ReleaseDate <= sawDate);
        return res;
    }
}
