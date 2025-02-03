using System;
using System.Threading.Tasks;
using crypto.Dtos;
using crypto.Models;
using crypto.Repositories;
using crypto.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Tests.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockConfiguration = new Mock<IConfiguration>();
            _userService = new UserService(_mockUserRepository.Object, _mockConfiguration.Object);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldCreateUser_WhenValidInput()
        {
            // Arrange
            var newUserDto = new NewUserDto("testuser", "password123");
            _mockUserRepository.Setup(repo => repo.GetUserByUsernameAsync(newUserDto.Username))
                .ReturnsAsync((User?)null);
            _mockUserRepository.Setup(repo => repo.SaveAsync(It.IsAny<User>()))
                .ReturnsAsync(new User { Username = newUserDto.Username });

            // Act
            var result = await _userService.CreateUserAsync(newUserDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newUserDto.Username, result.Username);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldThrowException_WhenUsernameIsMissing()
        {
            // Arrange
            var newUserDto = new NewUserDto("", "password123");

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _userService.CreateUserAsync(newUserDto));
        }

        [Fact]
        public async Task CreateUserAsync_ShouldThrowException_WhenPasswordIsMissing()
        {
            // Arrange
            var newUserDto = new NewUserDto("testuser", "");

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _userService.CreateUserAsync(newUserDto));
        }

        [Fact]
        public async Task CreateUserAsync_ShouldThrowException_WhenUsernameAlreadyExists()
        {
            // Arrange
            var newUserDto = new NewUserDto("testuser", "password123");
            _mockUserRepository.Setup(repo => repo.GetUserByUsernameAsync(newUserDto.Username))
                .ReturnsAsync(new User { Username = newUserDto.Username });

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _userService.CreateUserAsync(newUserDto));
        }
    }
}
