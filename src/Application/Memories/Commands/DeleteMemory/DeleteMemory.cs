using Microsoft.AspNetCore.Http;
using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.MovieRates.Commands.UpdateMemory;

public record DeleteMemoryCommand : IRequest<Unit>
{
    public int MemoryId { get; init; }
}

public class DeleteMemoryCommandHandler : IRequestHandler<DeleteMemoryCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public DeleteMemoryCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<Unit> Handle(DeleteMemoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Memories
               .Where(x => x.Id == request.MemoryId && x.UserId == _user.Id) // Add additional conditions if needed
               .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(request.MemoryId.ToString(), "Memory");
        }

        _context.Memories.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}


