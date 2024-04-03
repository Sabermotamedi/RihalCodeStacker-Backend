using System.Threading;
using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Application.Common.Models;
using Rihal.ReelRise.Application.Common.Security;
using Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;

namespace Rihal.ReelRise.Application.Movies.Queries;

[Authorize]
public record GetTopFiveByCurrentUserQuery : IRequest<List<GetTopFiveByCurrentUserDto>>;

public class GetTopFiveByCurrentUserHandler : IRequestHandler<GetTopFiveByCurrentUserQuery, List<GetTopFiveByCurrentUserDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;
    private readonly IMapper _mapper;

    public GetTopFiveByCurrentUserHandler(IApplicationDbContext context, IUser user, IMapper mapper)
    {
        _context = context;
        _user = user;
        _mapper = mapper;
    }

    public async Task<List<GetTopFiveByCurrentUserDto>> Handle(GetTopFiveByCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var movies = await _context.MovieRates.Include(x => x.Movie)
              .AsNoTracking()
              .Where(x => x.UserId == _user.Id)
              .OrderByDescending(t => t.Rate)
              .Take(5)
              .ProjectTo<GetTopFiveByCurrentUserDto>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);


        return movies;
    }
}
