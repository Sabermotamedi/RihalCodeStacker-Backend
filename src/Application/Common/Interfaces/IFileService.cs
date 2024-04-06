using Microsoft.AspNetCore.Http;
using Rihal.ReelRise.Application.Common.Models;

namespace Rihal.ReelRise.Application.Common.Interfaces;
public interface IFileService
{
    FileInformation GetFileInformation(IFormFile file);

    Task<(string PhotoName, string FilePath)> SaveToDisk(IFormFile photo);
}
