using Rihal.ReelRise.Application.Common.Interfaces;

namespace Rihal.ReelRise.Infrastructure.Storage.GoogleStorage;

public class GoogleStorageService : IGoogleStorageService
{
    //private readonly StorageClient _storageClient;
    //private readonly string _bucketName;

    //public GoogleStorageService()
    //{
    //    // Initialize the StorageClient with the provided credentials
    //    _storageClient = StorageClient.Create();// CreateFromJsonCredentialFile(credentialFilePath);
    //    _bucketName = "Rihal_Reels_Memories";
    //}

    //public async Task<string> UploadFileAsync(Stream stream, string fileName)
    //{
    //    try
    //    {
    //        // Upload the file to Google Cloud Storage
    //        var objectName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
    //        await _storageClient.UploadObjectAsync(_bucketName, objectName, null, stream);
    //        return objectName;
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle exceptions appropriately (e.g., logging)
    //        throw new Exception("An error occurred while uploading the file.", ex);
    //    }
    //}

    //public async Task<Stream> GetFileAsync(string fileName)
    //{
    //    try
    //    {
    //        // Download the file from Google Cloud Storage
    //        var stream = new MemoryStream();
    //        await _storageClient.DownloadObjectAsync(_bucketName, fileName, stream);
    //        stream.Position = 0;
    //        return stream;
    //    }
    //    catch (Google.GoogleApiException ex) when (ex.Error.Code == 404)
    //    {
    //        // File not found, handle appropriately
    //        throw new FileNotFoundException("File not found in Google Cloud Storage.", ex);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle other exceptions appropriately (e.g., logging)
    //        throw new Exception("An error occurred while downloading the file.", ex);
    //    }
    //}

    //public async Task DeleteFileAsync(string fileName)
    //{
    //    try
    //    {
    //        // Delete the file from Google Cloud Storage
    //        await _storageClient.DeleteObjectAsync(_bucketName, fileName);
    //    }
    //    catch (Google.GoogleApiException ex) when (ex.Error.Code == 404)
    //    {
    //        // File not found, handle appropriately
    //        throw new FileNotFoundException("File not found in Google Cloud Storage.", ex);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle other exceptions appropriately (e.g., logging)
    //        throw new Exception("An error occurred while deleting the file.", ex);
    //    }
    //}
    public Task DeleteFileAsync(string fileName)
    {
        throw new NotImplementedException();
    }

    public Task<Stream> GetFileAsync(string fileName)
    {
        throw new NotImplementedException();
    }

    public Task<string> UploadFileAsync(Stream stream, string fileName)
    {
        throw new NotImplementedException();
    }
}
