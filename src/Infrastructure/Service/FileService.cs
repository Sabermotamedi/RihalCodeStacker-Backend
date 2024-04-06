using Microsoft.AspNetCore.Http;
using Rihal.ReelRise.Application.Common.Interfaces;
using Rihal.ReelRise.Application.Common.Models;

namespace Rihal.ReelRise.Infrastructure.Service;
public class FileService : IFileService
{
    public async Task<(string PhotoName, string FilePath)> SaveToDisk(IFormFile photo)
    {
        var photoName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);

        var filePath = Path.Combine("c:\\files", photoName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await photo.CopyToAsync(stream); // Saving the photo to disk
        }

        return (photoName, filePath);
    }

    public FileInformation GetFileInformation(IFormFile file)
    {
        string fileName = Path.GetFileNameWithoutExtension(file.FileName);

        string fileExtension = Path.GetExtension(file.FileName);
        fileExtension = fileExtension.Contains(".") ? fileExtension.Replace(".", "") : fileExtension;

        long fileSize = file.Length;

        return new FileInformation(fileName, fileExtension, fileSize);
    }
}
