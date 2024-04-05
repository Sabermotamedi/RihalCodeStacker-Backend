using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Application.Common.Security;
using Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;
using Rihal.ReelRise.Application.Movies.Queries.SearchMovie;

namespace Rihal.ReelRise.Application.Movies.Queries;

[Authorize]
public record SearchMovieQuery : IRequest<List<SearchMovieDto>>
{
    public string? SearchValue { get; set; }
}

public class SearchMovieHandler : IRequestHandler<SearchMovieQuery, List<SearchMovieDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SearchMovieHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SearchMovieDto>> Handle(SearchMovieQuery request, CancellationToken cancellationToken)
    {
        if (request.SearchValue == null)
        {
            throw new ArgumentNullException(nameof(request.SearchValue));
        }

        var movies = await _context.Movies
              .AsNoTracking()
              .Where(x =>
                  (!string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(request.SearchValue.ToLower())) ||
                  (!string.IsNullOrEmpty(x.Description) && x.Description.ToLower().Contains(request.SearchValue.ToLower())))
              .ProjectTo<SearchMovieDto>(_mapper.ConfigurationProvider)
                  .ToListAsync(cancellationToken);

        return movies;
    }
}
