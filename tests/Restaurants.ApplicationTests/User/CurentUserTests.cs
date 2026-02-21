using FluentAssertions;
using Restaurants.Application.User;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.ApplicationTests.User
{
    public class CurentUserTests
    {
        //[Theory()]
        //[InlineData(UserRole.Admin)]
        //[InlineData(UserRole.Owner)]
        public void IsInRole_WithMatchingRole_ShoudReturnTrue( string role)
        {
            // Arrange

            var curentUser = new CurentUser("1", "test@gmail.com", [UserRole.Admin, UserRole.Owner], null, null);

            // Act

            var isinrole =  curentUser.IsInRole(role);


            // Assert
            isinrole.Should().BeTrue();

        }
       
    }


}