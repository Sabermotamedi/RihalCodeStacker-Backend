using System.Threading;
using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Application.Common.Models;
using Rihal.ReelRise.Application.Common.Security;
using Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;

namespace Rihal.ReelRise.Application.Movies.Queries;

[Authorize]
public record GetAllMovieWithRateQuery : IRequest<List<GetAllMovieWithRateDto>>;

public class GetAllMovieWithRateHandler : IRequestHandler<GetAllMovieWithRateQuery, List<GetAllMovieWithRateDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllMovieWithRateHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetAllMovieWithRateDto>> Handle(GetAllMovieWithRateQuery request, CancellationToken cancellationToken)
    {
        var movies = await _context.Movies
              .AsNoTracking()
              .ProjectTo<GetAllMovieWithRateDto>(_mapper.ConfigurationProvider)
              .OrderBy(t => t.Id)
              .ToListAsync(cancellationToken);

        //this is buttleneck and bug //What if we want to return 1 million records ? Pagination and pre calculation is needed
        movies.ForEach(movie => { movie.Description = LimitTo100Character(movie.Description); });

        return movies;
    }

    private string? LimitTo100Character(string? description)
    {
        string? result = string.Empty;
        if (description != null)
        {
            if (description.Length > 100)
            {
                string trimmedDescription = description.Substring(0, 96);
                int lastSpaceIndex = trimmedDescription.LastIndexOf(' ');

                if (lastSpaceIndex != -1)
                {
                    result = trimmedDescription.Substring(0, lastSpaceIndex);
                }

                result = result + " ...";
            }
            else
            {
                result = description;
            }
        }

        return result;
    }
}
