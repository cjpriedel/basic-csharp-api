using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SkillConnect.API.Controllers;
using SkillConnect.API.Models;
using SkillConnect.API.Services;
using SkillConnect.UnitTests.Fixtures;

namespace SkillConnect.UnitTests.Systems.Controllers;

public class UsersControllerTest
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        //Arrange
        var mockUsersService = new Mock<IUserService>();

        mockUsersService
           .Setup(x => x.GetAllUsers())
           .ReturnsAsync(new List<User>()
           {
                new()
                {
                    Id = 1,
                    Name = "jane",
                    Address = new Address()
                    {
                        Street = "123 Main St",
                        City = "San Diego",
                        ZipCode = "92120",
                    },
                    Email = "email@example.com"
                }
           }
           );

        var systemUnderTest = new UsersController(mockUsersService.Object);


        //Act
        var result = (OkObjectResult)await systemUnderTest.Get();

        //Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokesUserServiceExactlyOnce()
    {
        //Arrange
        var mockUsersService = new Mock<IUserService>();
        mockUsersService
            .Setup(x => x.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var systemUnderTest = new UsersController(mockUsersService.Object);


        //Act
        var result = await systemUnderTest.Get();

        //Assert
        mockUsersService.Verify( 
            x => x.GetAllUsers(), 
            Times.Once()
            );
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers()
    {
        //Arrange
        var mockUsersService = new Mock<IUserService>();
        mockUsersService
            .Setup(x => x.GetAllUsers())
            .ReturnsAsync(UsersFixtures.GetTestUsers());

        var systemUnderTest = new UsersController(mockUsersService.Object);


        //Act
        var result = await systemUnderTest.Get();

        //Assert

        result.Should().BeOfType<OkObjectResult>();
        var objResult = (OkObjectResult)result;
        objResult.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_On_No_Users_Found_Returns_404()
    {
        //Arrange
        var mockUsersService = new Mock<IUserService>();
        mockUsersService
            .Setup(x => x.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var systemUnderTest = new UsersController(mockUsersService.Object);


        //Act
        var result = await systemUnderTest.Get();

        //Assert
        result.Should().BeOfType<NotFoundResult>();
        var objResult = (NotFoundResult)result;
        objResult.StatusCode.Should().Be(404);
    }

}