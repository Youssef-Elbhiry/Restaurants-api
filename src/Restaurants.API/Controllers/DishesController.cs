using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishs.Commands.CreateDish;
using Restaurants.Application.Dishs.Commands.DeleteDishFromRestaurant;
using Restaurants.Application.Dishs.Dtos;

using Restaurants.Application.Dishs.Queries.GetAllDishesForRestaurant;
using Restaurants.Application.Dishs.Queries.GetDishById;

namespace Restaurants.API.Controllers
{
    [Route("api/Restaurant/{RestaurantId}/[controller]")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy ="AtLeast20")]
        public async Task<IActionResult> GetDishesForRestaurant(int RestaurantId)
        {
            var dishes = await mediator.Send(new GetAllDishesForRestaurantQuery(RestaurantId));

            return Ok(dishes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish(int RestaurantId, CreateDishCommand command)
        {
            command.RestaurantId = RestaurantId;

            int id = await mediator.Send(command);

            return CreatedAtAction(nameof(GetDishById), new { RestaurantId = RestaurantId, DishId = id }, null);

        }

        [HttpGet("{DishId:int}")]
        public async Task<IActionResult> GetDishById(int RestaurantId, int DishId)
        {
            var dish = await mediator.Send(new GetDishByIdQuery(RestaurantId, DishId));

            if (dish == null)
            {
                return NotFound();
            }
            return Ok(dish);
        }

        [HttpDelete("{DishId:int}")]

        public async Task<IActionResult> DeleteDish(int RestaurantId, int DishId)
        {
           await mediator.Send(new DeleteDishFromRestaurantCommand(RestaurantId, DishId));

            return NoContent();
        }
    }
}
