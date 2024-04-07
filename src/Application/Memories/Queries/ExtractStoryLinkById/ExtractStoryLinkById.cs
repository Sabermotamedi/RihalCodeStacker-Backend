using System.Threading;
using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Application.Common.Models;
using Rihal.ReelRise.Application.Common.Security;
using Rihal.ReelRise.Application.Movies.Queries.GetAllMovieWithRate;

namespace Rihal.ReelRise.Application.Movies.Queries;

[Authorize]
public record ExtractStoryLinkByIdQuery : IRequest<ExtractStoryLinkByIdDto>
{
    public int Id { get; set; }
}

public class ExtractStoryLinkByIdHandler : IRequestHandler<ExtractStoryLinkByIdQuery, ExtractStoryLinkByIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ITextService _textService;

    public ExtractStoryLinkByIdHandler(IApplicationDbContext context, ITextService textService)
    {
        _context = context;
        _textService = textService;
    }

    public async Task<ExtractStoryLinkByIdDto> Handle(ExtractStoryLinkByIdQuery request, CancellationToken cancellationToken)
    {
        var memory = await _context.Memories
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (memory == null)
        {
            throw new NotFoundException(request.Id.ToString(), "Memory");
        }

        ExtractStoryLinkByIdDto StoryUrls = new() { Id = request.Id };

        if (memory.Story is not null)
        {
            StoryUrls.Links = _textService.ExtractUrls(memory.Story);
            return StoryUrls;
        }

        return StoryUrls;
    }
}
