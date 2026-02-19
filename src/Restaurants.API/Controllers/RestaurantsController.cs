using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Restaurants.API.Constants;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Commands.UploadRestaurantLoge;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurant.GetRestaurantById;
using System.Reflection.Metadata.Ecma335;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController( IMediator mediator) : ControllerBase
    {
      //  [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(IEnumerable<RestaurantsDto>))]
        public async Task<IActionResult> GetAll([FromQuery] GetAllRestaurantQuery query)
        {
           var restaurants = await mediator.Send(query) ;
              return Ok(restaurants);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = UserRole.Owner)]
        public async Task<IActionResult> GetById(int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));

            if(restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Roles =UserRole.Owner)]
        public async Task<IActionResult> Create(CreateRestaurantCommand command)
        {
            if(ModelState.IsValid)
            {
              int id = await  mediator.Send(command);

                return CreatedAtAction(nameof(GetById),new {id},null);
              }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Policy = "AtLeast2Restaurants")]
        public async Task<IActionResult> Delete(int id)
        {
             await mediator.Send(new DeleteRestaurantCommand(id));

            return NoContent();

        }
        [Authorize(Policy = "Nationality")]
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id , UpdateRestaurantCommand command)
        {
            command.Id = id;

            await mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{id:int}/Logo")]
        [Authorize(Roles = UserRole.Owner)]
        public async Task<IActionResult> UploadLogo([FromRoute]int id ,IFormFile file)
        {
            var stream = file.OpenReadStream();

            var command = new UploadRestaurantLogoCommand
            {
                RestaurantId = id,
                FileName = file.FileName,
                File = stream
            };

             await mediator.Send(command);
            return NoContent();
        }


    }
}
