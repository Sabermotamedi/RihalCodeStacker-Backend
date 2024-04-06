using Microsoft.AspNetCore.Http;
using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Domain.Entities;

namespace Rihal.ReelRise.Application.MovieRates.Commands.CreateMemory;

public record CreateMemoryCommand : IRequest<int>
{
    public int MovieId { get; init; }

    public string? Title { get; set; }

    public string? Story { get; set; }

    public DateTime SawDate { get; set; }

    public List<IFormFile>? Photos { get; set; }
}

public class CreateMemoryCommandHandler : IRequestHandler<CreateMemoryCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;
    private readonly IS3Storage _s3Storage;

    public CreateMemoryCommandHandler(IApplicationDbContext context, IUser user, IS3Storage s3Storage)
    {
        _context = context;
        _user = user;
        _s3Storage = s3Storage;
    }

    public async Task<int> Handle(CreateMemoryCommand request, CancellationToken cancellationToken)
    {
        Memory entity = new Memory()
        {
            UserId = _user.Id,
            MovieId = request.MovieId,
            Title = request.Title,
            Story = request.Story,
            SawDate = request.SawDate
        };

        if (request.Photos != null && request.Photos.Count > 0)
        {
            entity.MemoryPhotos = new List<MemoryPhoto>();

            foreach (var photo in request.Photos)
            {
                var result = await SaveToDisk(photo);

                // Fire and forget
                _ = SaveToS3(result);

                MemoryPhoto MemoryPhoto = new()
                {
                    Name = result.PhotoName
                };

                entity.MemoryPhotos.Add(MemoryPhoto);
            }
        }

        _context.Memories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    private async Task SaveToS3((string PhotoName, string FilePath) result)
    {
        await _s3Storage.UploadToS3Async(result.PhotoName, result.FilePath, "MemoryFiles");
    }

    private static async Task<(string PhotoName, string FilePath)> SaveToDisk(IFormFile photo)
    {
        var photoName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);

        var filePath = Path.Combine("c:\\files", photoName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await photo.CopyToAsync(stream); // Saving the photo to disk
        }

        return (photoName, filePath);
    }
}
