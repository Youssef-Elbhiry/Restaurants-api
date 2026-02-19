using MediatR;

namespace Restaurants.Application.Restaurants.Commands.UploadRestaurantLoge;

public class UploadRestaurantLogoCommand : IRequest
{
    public int RestaurantId { get; set; }
    public string FileName { get; set; }
    public Stream File { get; set; }
}
