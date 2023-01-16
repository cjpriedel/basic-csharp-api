﻿using Moq;
using Newtonsoft.Json;
using Moq.Protected;
using SkillConnect.API.Models;

namespace SkillConnect.UnitTests.Helpers
{
    internal static class MockHttpMessageHandler<T>
    {
        internal static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResponse)
        {
            var mockResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
            };

            mockResponse.Content.Headers.ContentType = 
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync", 
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);

            return handlerMock;
        }

        internal static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<User> expectedResponse, string endpoint)
        {
            var mockResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
            };

            mockResponse.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var handlerMock = new Mock<HttpMessageHandler>();

            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(endpoint),
                Method = HttpMethod.Get,
            };

            handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);

            return handlerMock;
        }

        internal static Mock<HttpMessageHandler> SetupReturn404(List<User> expectedResponse)
        {
            var mockResponse = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
            {
                Content = new StringContent("")
            };

            mockResponse.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync", 
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);

            return handlerMock;
        }
    }
}
