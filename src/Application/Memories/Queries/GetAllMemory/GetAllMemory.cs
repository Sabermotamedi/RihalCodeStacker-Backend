using System.Threading;
using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Application.Common.Models;
using Rihal.ReelRise.Application.Common.Security;
using Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;

namespace Rihal.ReelRise.Application.Movies.Queries;

[Authorize]
public record GetAllMemoryQuery : IRequest<List<GetAllMemoryDto>>;

public class GetAllMemoryHandler : IRequestHandler<GetAllMemoryQuery, List<GetAllMemoryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllMemoryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetAllMemoryDto>> Handle(GetAllMemoryQuery request, CancellationToken cancellationToken)
    {
        var movies = await _context.Memories
                            .Include(x => x.Movie)
                            .Include(x=>x.MemoryPhotos)
                            .AsNoTracking()
                              .ProjectTo<GetAllMemoryDto>(_mapper.ConfigurationProvider)
                                 .ToListAsync(cancellationToken);

        return movies;
    }
}
