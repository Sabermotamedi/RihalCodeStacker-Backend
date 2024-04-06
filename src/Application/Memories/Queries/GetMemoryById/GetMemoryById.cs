using System.Threading;
using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Application.Common.Models;
using Rihal.ReelRise.Application.Common.Security;
using Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;

namespace Rihal.ReelRise.Application.Movies.Queries;

[Authorize]
public record GetMemoryByIdQuery : IRequest<GetMemoryByIdDto>
{
    public int Id { get; set; }
}

public class GetMemoryByIdHandler : IRequestHandler<GetMemoryByIdQuery, GetMemoryByIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMemoryByIdHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetMemoryByIdDto> Handle(GetMemoryByIdQuery request, CancellationToken cancellationToken)
    {
        var movies = await _context.Memories
                            .Include(x => x.Movie)
                            .Include(x => x.MemoryPhotos)
                            .AsNoTracking()
                            .Where(x => x.Id == request.Id)
                              .ProjectTo<GetMemoryByIdDto>(_mapper.ConfigurationProvider)
                                 .FirstOrDefaultAsync(cancellationToken);

        return movies != null ? movies : new GetMemoryByIdDto();
    }
}
