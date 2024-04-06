using MediatR;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.MovieRates.Commands.UpdateMemory;

public record UpdateOrDeleteMemoryPhotoCommand : IRequest<Unit>
{
    public int MemoryId { get; init; }

    public List<int>? PhotoIds { get; set; }

    public IFormFileCollection? Photos { get; set; }
}

public class UpdateOrDeleteMemoryPhotoCommandHandler : IRequestHandler<UpdateOrDeleteMemoryPhotoCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;
    private readonly IFileService _fileService;
    private readonly IS3Storage _s3Storage;

    public UpdateOrDeleteMemoryPhotoCommandHandler(IApplicationDbContext context, IUser user, IS3Storage s3Storage, IFileService fileService)
    {
        _context = context;
        _user = user;
        _s3Storage = s3Storage;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(UpdateOrDeleteMemoryPhotoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //==================== Delete
            if (request.PhotoIds is not null && request.PhotoIds.Count > 0)
            {
                foreach (var photoId in request.PhotoIds)
                {
                    await DeletePhotosAsync(photoId, cancellationToken);
                }
            }

            //====================== Update New Photo
            if (request.Photos != null && request.Photos.Count > 0)
            {
                var entity = await _context.Memories
                    .Include(x => x.MemoryPhotos)
                    .FirstOrDefaultAsync(x => x.Id == request.MemoryId);

                if (entity != null)
                {
                    if (entity.MemoryPhotos is null)
                    {
                        entity.MemoryPhotos = new List<MemoryPhoto>();
                    }

                    foreach (var photo in request.Photos)
                    {
                        var fileInfo = _fileService.GetFileInformation(photo);
                        var result = await _fileService.SaveToDisk(photo);
                        // Fire and forget
                        _ = SaveToS3(result.PhotoName, result.FilePath);

                        MemoryPhoto MemoryPhoto = new()
                        {
                            UniqueName = result.PhotoName,
                            Name = fileInfo.Name,
                            Extension = fileInfo.Extension,
                            Size = fileInfo.Size
                        };

                        entity.MemoryPhotos.Add(MemoryPhoto);
                    }
                }
                else
                {
                    throw new NotFoundException(request.MemoryId.ToString(), "Memory");
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
        catch (Exception)
        {
            // Rollback changes by detaching all tracked entities
            _context.ChangeTracker.Entries().ToList().ForEach(entry => entry.State = EntityState.Detached);

            throw;
        }
    }

    private async Task DeletePhotosAsync(int photoId, CancellationToken cancellationToken)
    {

        var entity = await _context.MemoryPhoto
                           .Where(x => x.Id == photoId)
                               .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(photoId.ToString(), "Photo");
        }
        _context.MemoryPhoto.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task SaveToS3(string PhotoName, string FilePath)
    {
        await _s3Storage.UploadToS3Async(PhotoName, FilePath, "MemoryFiles");
    }
}


