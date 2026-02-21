namespace Restaurants.Domain.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadToBlobAsync(Stream data, string filename);
    string? GetBlobSasUrl(string? bloburl);
}
