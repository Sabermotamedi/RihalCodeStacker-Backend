using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihal.ReelRise.Application.Common.Interfaces;
public interface IGoogleStorageService
{
    Task<string> UploadFileAsync(Stream stream, string fileName);

    Task<Stream> GetFileAsync(string fileName);

    Task DeleteFileAsync(string fileName);
}
