using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.Repositories;

public class UserRepository_Tests
{
    private readonly DataContext _context =
    new(new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public void Create_ShouldCreateUser()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userEntity = new UserEntity { FirstName = "Test Firstname", LastName = "Test Lastname", Email = "test@example.com" };


        // Act
        var result = userRepository.Create(userEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }

    [Fact]
    public void Get_ShouldGetAllUsers()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userEntity = new UserEntity { FirstName = "Test Firstname", LastName = "Test Lastname", Email = "test@example.com" };
        userRepository.Create(userEntity);


        // Act
        var result = userRepository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<UserEntity>>(result);
        Assert.Single(result);


    }

    [Fact]
    public void Get_ShouldGetOneUser()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userEntity = new UserEntity { FirstName = "Test Firstname", LastName = "Test Lastname", Email = "test@example.com" };
        userRepository.Create(userEntity);


        // Act
        var result = userRepository.Get(x => x.Email == userEntity.Email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userEntity.Email, result.Email);


    }

    [Fact]
    public void Update_ShouldUpdateOneUser()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userEntity = new UserEntity { FirstName = "Test Firstname", LastName = "Test Lastname", Email = "test@example.com" };
        userEntity = userRepository.Create(userEntity);

        // Act
        userEntity.Email = "Updated user";
        var result = userRepository.Update(x => x.Id == userEntity.Id, userEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userEntity.Id, result.Id);
        Assert.Equal(userEntity.Email, result.Email);



    }

    [Fact]
    public void Delete_ShouldDeleteOneUser()
    {
        // Arrange
        var userRepository = new UserRepository(_context);
        var userEntity = new UserEntity { FirstName = "Test Firstname", LastName = "Test Lastname", Email = "test@example.com" };
        userRepository.Create(userEntity);

        // Act
        var result = userRepository.Delete(x => x.Id == userEntity.Id);

        // Assert
        Assert.True(result);



    }
}
