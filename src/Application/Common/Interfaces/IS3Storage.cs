using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihal.ReelRise.Application.Common.Interfaces;
public interface IS3Storage
{
    public Task<bool> UploadToS3Async(string objectKey, string filePath, string cloudPath);

    public Task<bool> CreateS3FolderAsync(string folderName);

    public Task<bool> DeleteS3FolderAsync(string folderName);

    public Task<bool> CheckFolderExistsAsync(string folderKey);

    public Task<bool> CheckFileExistsAsync(string fileName, string filePatch);
}
