using Microsoft.AspNetCore.Http;
using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.MovieRates.Commands.UpdateMemory;

public record DeleteMemoryPhotoCommand : IRequest<Unit>
{
    public int PhotoId { get; init; }
}

public class DeleteMemoryPhotoCommandHandler : IRequestHandler<DeleteMemoryPhotoCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IS3Storage _s3Bucket;

    public DeleteMemoryPhotoCommandHandler(IApplicationDbContext context, IS3Storage s3Bucket)
    {
        _context = context;
        _s3Bucket = s3Bucket;
    }

    public async Task<Unit> Handle(DeleteMemoryPhotoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.MemoryPhoto
         .Where(x => x.Id == request.PhotoId)
         .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(request.PhotoId.ToString(), "Photo");
        }

        try
        {
            _context.MemoryPhoto.Remove(entity);

            RemoveFromDisk(entity);

            RemoveFromCloud(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        catch (Exception)
        {
            // Rollback changes by detaching all tracked entities
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }

            throw;
        }
    }

    private void RemoveFromCloud(MemoryPhoto entity)
    {
    }

    private void RemoveFromDisk(MemoryPhoto entity)
    {
        
    }
}


