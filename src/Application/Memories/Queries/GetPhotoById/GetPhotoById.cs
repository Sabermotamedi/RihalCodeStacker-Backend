using Microsoft.AspNetCore.Mvc;
using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Application.Common.Security;
using Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;

namespace Rihal.ReelRise.Application.Movies.Queries;

[Authorize]
public record GetPhotoByIdQuery : IRequest<FileStream>
{
    public int Id { get; set; }
}

public class GetPhotoByIdHandler : IRequestHandler<GetPhotoByIdQuery, FileStream>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPhotoByIdHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FileStream> Handle(GetPhotoByIdQuery request, CancellationToken cancellationToken)
    {
        var memoryPhoto = await _context.MemoryPhoto
                                    .AsNoTracking()
                                    .Where(x => x.Id == request.Id)
                                         .FirstOrDefaultAsync(cancellationToken);

        if (memoryPhoto != null && !string.IsNullOrEmpty(memoryPhoto.UniqueName))
        {
            var filePath = Path.Combine("c:\\files", memoryPhoto.UniqueName);

            return await Task.FromResult(new FileStream(filePath, FileMode.Open, FileAccess.Read));
        }
        else
        {
            throw new FileNotFoundException($"File '{request.Id}' not found.");
        }
    }
}
