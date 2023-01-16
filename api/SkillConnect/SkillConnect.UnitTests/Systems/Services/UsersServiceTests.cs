using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using SkillConnect.API.Config;
using SkillConnect.API.Models;
using SkillConnect.API.Services;
using SkillConnect.UnitTests.Fixtures;
using SkillConnect.UnitTests.Helpers;

namespace SkillConnect.UnitTests.Systems.Services
{
    public class UsersServiceTests
    {
        [Fact]
        public async Task Get_all_users_when_called_Invokes_HTTP_Get()
        {
            //Arrange
            var expectedResponse = UsersFixtures.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";
            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = endpoint
                }
                );
            var systemUnderTest = new UsersService(httpClient, config);


            //Act
            await systemUnderTest.GetAllUsers();


            //Assert
            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(
                    x => x.Method == HttpMethod.Get), 
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetAllUsers_when_called_returns_list_of_all_users()
        {
            //Arrange
            var expectedResponse = UsersFixtures.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse);
            var endpoint = "https://example.com/users";
            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = endpoint
                }
                );
            var httpClient = new HttpClient(handlerMock.Object);

            var systemUnderTest = new UsersService(httpClient, config);


            //Act
            var result = await systemUnderTest.GetAllUsers();


            //Assert
            result.Count.Should().Be(expectedResponse.Count);
        }

        [Fact]
        public async Task GetAllUsers_when_called_returns_list_of_expected_size()
        {
            //Arrange
            var expectedResponse = UsersFixtures.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse);
            var endpoint = "https://example.com/users";
            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = endpoint
                }
                );
            var httpClient = new HttpClient(handlerMock.Object);

            var systemUnderTest = new UsersService(httpClient, config);


            //Act
            var result = await systemUnderTest.GetAllUsers();


            //Assert
            result.Count.Should().Be(expectedResponse.Count);
        }

        [Fact]
        public async Task GetAllUsers_when_called_invokes_configured_external_url()
        {
            //Arrange
            var expectedResponse = UsersFixtures.GetTestUsers();
            var endpoint = "https://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse, endpoint);
            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = endpoint
                });

            var httpClient = new HttpClient(handlerMock.Object);
            var systemUnderTest = new UsersService(httpClient, config);


            //Act
            var result = await systemUnderTest.GetAllUsers();

            var uri = new Uri(endpoint);

            //Assert
            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(
                    x => x.Method == HttpMethod.Get && 
                    x.RequestUri == uri
                    ), ItExpr.IsAny<CancellationToken>());
        }
    }
}
