using Azure.Storage.Blobs;
using Azure.Storage.Sas;
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

    public string? GetBlobSasUrl(string? bloburl)
    {
        if(bloburl is null) return null;

        var sasbuilder = new BlobSasBuilder()
        {
          BlobContainerName = blobStorageSettings.LogoContainerName,
          Resource = "b",
          StartsOn = DateTimeOffset.UtcNow,
          ExpiresOn = DateTimeOffset.UtcNow.AddHours(1),
          BlobName = GetBlobName(bloburl)
        };

        sasbuilder.SetPermissions(BlobSasPermissions.Read);

        var blobserviceclient = new BlobServiceClient(blobStorageSettings.ConnectionString);
  

   var sastoken = sasbuilder.
            ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(blobserviceclient.AccountName, blobStorageSettings.AccountKey))
            .ToString();

        return $"{bloburl}?{sastoken}";
    }

    private string GetBlobName(string bloburl)
    {
       var uri = new Uri(bloburl);
         return uri.Segments.Last();
    }
}
