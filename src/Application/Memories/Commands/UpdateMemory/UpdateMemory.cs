using Microsoft.AspNetCore.Http;
using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.MovieRates.Commands.UpdateMemory;

public record UpdateMemoryCommand : IRequest<Unit>
{
    public int MemoryId { get; init; }

    public string? Title { get; set; }

    public string? Story { get; set; }
}

public class UpdateMemoryCommandHandler : IRequestHandler<UpdateMemoryCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public UpdateMemoryCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<Unit> Handle(UpdateMemoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Memories
            .FirstOrDefaultAsync(x => x.Id == request.MemoryId
                                   && x.UserId == _user.Id);

        if (entity == null)
        {
            throw new NotFoundException(request.MemoryId.ToString(), "Memory");
        }

        entity.Title = request.Title;

        entity.Story = request.Story;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}


