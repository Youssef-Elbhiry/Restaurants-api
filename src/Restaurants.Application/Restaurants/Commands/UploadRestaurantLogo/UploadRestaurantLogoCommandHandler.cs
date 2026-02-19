using MediatR;
using Restaurants.Application.Restaurants.Commands.UploadRestaurantLoge;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UploadRestaurantLogo;

public class UploadRestaurantLogoCommandHandler(IRestaurantsRepository restaurantsRepository
    ,IRestaurantAuthorizationService restaurantAuthorizationService,
   IBlobStorageService blobStorageService ) : IRequestHandler<UploadRestaurantLogoCommand>
{
    public async Task Handle(UploadRestaurantLogoCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
        if (restaurant is null)
            throw new NotFoundException(nameof(restaurant), request.RestaurantId.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update)) 
            throw new ForbidException();

       var logouri =  await blobStorageService.UploadToBlobAsync(request.File , request.FileName);

        restaurant.LogoUri = logouri;
      await  restaurantsRepository.UpdateRestaurantAsync(restaurant);
    }
}
