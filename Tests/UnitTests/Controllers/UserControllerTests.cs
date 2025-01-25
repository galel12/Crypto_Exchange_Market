using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using crypto.Controllers;
using crypto.Dtos;
using crypto.Models;
using crypto.Services;

namespace Tests.UnitTests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);
        }
       [Fact]
        public async Task CreateUser_ShouldReturnCreatedStatus()
        {
            // Arrange
            var newUserDto = new NewUserDto("testuser", "password123");

            var createdUser = new User
            {
                Id = 1,
                Username = "testuser"
            };

            _mockUserService.Setup(service => service.CreateUserAsync(newUserDto))
                .ReturnsAsync(createdUser);

            // Act
            var result = await _userController.CreateUser(newUserDto) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(createdUser, result.Value);
        }
    }
}