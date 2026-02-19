using Xunit;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void CreateRestaurantCommandValidator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var command = new CreateRestaurantCommand
            {
                Name = "Test Restaurant",
                Description = "A test restaurant",
                ContactEmail = "test@gmail.com",
                Category = "Indian"
            };

            var validator = new CreateRestaurantCommandValidator();

            // Act

            var result = validator.TestValidate(command);


            // Assert

            result.ShouldNotHaveAnyValidationErrors();

        }

        [Fact()]
        public void CreateRestaurantCommandValidator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
           //Arrange 

            var command = new CreateRestaurantCommand
            {
                Name = "",
                Description = "A test restaurant",
                ContactEmail = "invalid-email",
                Category = "spanish"
            };

            var validator = new CreateRestaurantCommandValidator();
           
            // Act
            var result = validator.TestValidate(command);


            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c => c.Category);

        }
    }
}