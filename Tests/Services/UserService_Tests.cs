using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using DbAssignment.Services;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;

public class UserService_Tests
{

    private readonly DataContext _context =
       new(new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase($"{Guid.NewGuid()}")
           .Options);


    [Fact]
    public void CreateUser_ShouldCreateUser()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var user = new UserService(userRepository);
        string firstName = "Test user";
        string lastName = "Test user";
        string email = "Test email";

        // Act
        var result = user.CreateUser(firstName, lastName, email);

        // Assert

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
    [Fact]
    public void CreateUser_ShouldReturnNull_WhenUserNameAlreadyExists()
    {
        // Arrange
        var userService = new UserService(new UserRepository(_context));
        userService.CreateUser("Existing user", "Existing user", "Existing user");

        // Act
        var result = userService.CreateUser("Existing user", "Existing user", "Existing user");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllUsers_ShouldReturnAllUsers()
    {
        // Arrange
        var userService = new UserService(new UserRepository(_context));
        userService.CreateUser("User 1", "User 1", "User1@email.com");
        userService.CreateUser("User 2", "User 2", "User2@email.com");
        userService.CreateUser("User 3", "User 3", "User3@email.com");

        // Act
        var result = userService.GetAllUsers();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void GetUserByUserId_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var userService = new UserService(new UserRepository(_context));
        userService.CreateUser("Existing user", "Existing user", "Existing user");

        // Act
        var result = userService.GetUserById(1);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void GetUserByUserId_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);

        // Act
        var result = userService.GetUserById(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateUser_ShouldUpdateUser_WhenUserExists()
    {
        // Arrange
        var userService = new UserService(new UserRepository(_context));
        var existingUser = userService.CreateUser("Existing firstname", "Existing lastname", "Existing email");
        existingUser.FirstName = "Updated user";

        // Act
        var result = userService.UpdateUser(existingUser);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated user", result.FirstName);
    }

    [Fact]
    public void UpdateUser_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var nonExistingUser = new UserEntity { Id = 999, Email = "Non-existing user" }; // 999 is a non-existing Id

        // Act
        var result = userService.UpdateUser(nonExistingUser);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteUser_ShouldDeleteUser_WhenUserExists()
    {
        // Arrange
        var userService = new UserService(new UserRepository(_context));
        var existingUser = userService.CreateUser("Existing firstname", "Existing lastname", "Existing email");

        // Act
        userService.DeleteUser(existingUser.Id);
        var result = userService.GetUserById(existingUser.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteUser_ShouldNotThrowException_WhenUserDoesNotExist()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);

        // Act
        var exception = Record.Exception(() => userService.DeleteUser(999)); // 999 is a non-existing Id

        // Assert
        Assert.Null(exception);
    }
}
