using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Configuration;

namespace Restaurants.Infrastructure.Storage;

public class BlobStorageService(IOptions<BlobStorageSettings> blobstoragesettingsoptions) : IBlobStorageService
{
    private readonly  BlobStorageSettings blobStorageSettings = blobstoragesettingsoptions.Value;
    public async Task<string> UploadToBlobAsync(Stream data , string filename)
    {
        var blobserviceclient = new BlobServiceClient(blobStorageSettings.ConnectionString);

        var containerclient = blobserviceclient.GetBlobContainerClient(blobStorageSettings.LogoContainerName);

        var blobclient = containerclient.GetBlobClient(filename);

       await blobclient.UploadAsync(data);

        return blobclient.Uri.ToString();
    }
}
