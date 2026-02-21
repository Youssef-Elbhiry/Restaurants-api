namespace Restaurants.Infrastructure.Configuration;

public class BlobStorageSettings
{
    public string ConnectionString { get; set; }
    public string LogoContainerName { get; set; }
    public string AccountKey { get; set; }
}
