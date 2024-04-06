using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Rihal.ReelRise.Application.Common.Interfaces;

namespace Rihal.ReelRise.Infrastructure.Storage.AwsStorage;
public class S3Storage : IS3Storage
{

    private readonly string? awsAccessKey;
    private readonly string? awsSecretKey;
    private readonly string? bucketName;
    private readonly RegionEndpoint? regionEndpoint;

    public AmazonS3Client? s3Client;

    public S3Storage()
    {

        awsAccessKey = "000";
        awsSecretKey = "00000";
        bucketName = "RihalReels.Photos";
        regionEndpoint = RegionEndpoint.USEast1;
    }

    public async Task<bool> UploadToS3Async(string objectKey, string filePath, string cloudPath)
    {
        string patchAndFileName = CombineObjectKeyAndcloudPath(objectKey, cloudPath);

        using (s3Client = new AmazonS3Client(awsAccessKey, awsSecretKey, regionEndpoint))
        {
            try
            {
                var transferUtility = new TransferUtility(s3Client);
                await transferUtility.UploadAsync(filePath, bucketName, patchAndFileName);

                return true;
            }
            catch (AmazonS3Exception)
            {

                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }

    public async Task<bool> CheckFileExistsAsync(string fileName, string filePatch)
    {
        using (s3Client = new AmazonS3Client(awsAccessKey, awsSecretKey, regionEndpoint))
        {
            try
            {
                var objectRequest = new GetObjectMetadataRequest
                {
                    BucketName = bucketName,
                    Key = string.Format("{0}/{1}", filePatch, fileName)
                };

                var response = await s3Client.GetObjectMetadataAsync(objectRequest);

                return true;
            }
            catch (AmazonS3Exception ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    return false;

                }
            }
            catch (Exception)
            {
                return false;


            }
        }
    }

    public async Task<bool> CreateS3FolderAsync(string folderName)
    {
        using (s3Client = new AmazonS3Client(awsAccessKey, awsSecretKey, regionEndpoint))
        {
            try
            {
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = string.Format("{0}/", folderName),
                    ContentBody = string.Empty
                };

                var response = await s3Client.PutObjectAsync(request);

                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }

    public async Task<bool> DeleteS3FolderAsync(string folderName)
    {
        using (s3Client = new AmazonS3Client(awsAccessKey, awsSecretKey, regionEndpoint))
        {
            try
            {
                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = string.Format("{0}/", folderName)
                };

                var res = await s3Client.DeleteObjectAsync(deleteRequest);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public async Task<bool> CheckFolderExistsAsync(string folderKey)
    {
        using (s3Client = new AmazonS3Client(awsAccessKey, awsSecretKey, regionEndpoint))
        {
            try
            {
                var request = new ListObjectsV2Request
                {
                    BucketName = bucketName,
                    Prefix = string.Format("{0}/", folderKey)
                };

                ListObjectsV2Response response = await s3Client.ListObjectsV2Async(request);

                var res = response.S3Objects.Count > 0;
                return res;
            }
            catch (AmazonS3Exception ex)
            {

                var a = ex.Message;
                return false;

            }
            catch (Exception ex)
            {
                var ms = ex.Message;
                throw;
            }
        }
    }

    private string CombineObjectKeyAndcloudPath(string objectKey, string cloudPath)
    {
        return string.Format("{0}/{1}", cloudPath, objectKey);
    }
}

