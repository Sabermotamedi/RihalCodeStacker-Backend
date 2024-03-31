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

    public async Task<int> Handle(CreateMovieRateCommand request, CancellationToken cancellationToken)
    {
        MovieRate entity = new MovieRate(_user.Id)
        {
            MovieId = request.MovieId,
            Rate = request.Rate
        };

        _context.MovieRates.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
