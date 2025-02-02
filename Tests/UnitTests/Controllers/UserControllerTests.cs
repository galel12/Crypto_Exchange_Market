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

        [Fact]
        public async Task CreateUser_ShouldReturnBadRequest_WhenUsernameIsMissing()
        {
            // Arrange
            var newUserDto = new NewUserDto("", "password123");

            _mockUserService.Setup(service => service.CreateUserAsync(newUserDto))
                .ThrowsAsync(new ArgumentException("Username is required"));

            // Act
            var result = await _userController.CreateUser(newUserDto) as BadRequestObjectResult;
       
            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Username is required", result.Value);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnBadRequest_WhenPasswordIsMissing()
        {
            // Arrange
            var newUserDto = new NewUserDto("testuser", "");
            _mockUserService.Setup(service => service.CreateUserAsync(newUserDto))
                .ThrowsAsync(new ArgumentException("Password is required"));

            // Act
            var result = await _userController.CreateUser(newUserDto) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Password is required", result.Value);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnConflict_WhenUsernameAlreadyExists()
        {
            // Arrange
            var newUserDto = new NewUserDto("testuser", "password123");

            _mockUserService.Setup(service => service.CreateUserAsync(newUserDto))
                .ThrowsAsync(new InvalidOperationException("Username already exists"));

            // Act
            var result = await _userController.CreateUser(newUserDto) as ConflictObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(409, result.StatusCode);
            Assert.Equal("Username already exists", result.Value);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnInternalServerError_OnUnexpectedException()
        {
            // Arrange
            var newUserDto = new NewUserDto("testuser", "password123");

            _mockUserService.Setup(service => service.CreateUserAsync(newUserDto))
                .ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _userController.CreateUser(newUserDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("An error occurred while creating the user.", result.Value);
        }
    }
}