using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.MovieRates.Commands.CreateMovieRate;

public record CreateMovieRateCommand : IRequest<int>
{
    public int MovieId { get; init; }
    public int Rate { get; set; }
}

public class CreateMovieRateCommandHandler : IRequestHandler<CreateMovieRateCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public CreateMovieRateCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }
    //What if our user add duplicate rate for a movie? //Bug
    public async Task<int> Handle(CreateMovieRateCommand request, CancellationToken cancellationToken)
    {
        MovieRate entity = new MovieRate()
        {
            UserId = _user.Id,
            MovieId = request.MovieId,
            Rate = request.Rate
        };

        _context.MovieRates.Add(entity);

        var movie = await _context.Movies.Include(x => x.MovieRates).FirstOrDefaultAsync(x => x.Id == request.MovieId);

        if (movie is not null && movie.MovieRates is not null && movie.MovieRates.Count>0)
        {
            int rate = 0;
            int avgRate = 0;
            foreach (var movieRate in movie.MovieRates)
            {
                rate += movieRate.Rate;
            }

            avgRate = rate / movie.MovieRates.Count;
            movie.AverageRate = avgRate;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
