using FluentValidation;
using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Application.MovieRates.Commands.CreateMemory;
using Rihal.ReelRise.Application.MovieRates.Commands.CreateMovieRate;
using Rihal.ReelRise.Application.MovieRates.Commands.UpdateMemory;

namespace Rihal.ReelRise.Application.TodoLists.Commands.UpdateMemory;

public class UpdateMemoryCommandValidator : AbstractValidator<UpdateMemoryCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;


    public UpdateMemoryCommandValidator(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;

        RuleFor(v => v.Title)           
            .NotEmpty();

        RuleFor(v => v.Story)
          .NotEmpty();
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
}
