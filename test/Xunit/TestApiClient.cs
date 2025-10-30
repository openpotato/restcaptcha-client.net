#region RESTCaptcha .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    RESTCaptcha .NET Client
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using Xunit;

namespace RestCaptcha.Client.Tests
{
    /// <summary>
    /// Unit tests for <see cref="ApiClient"/>.
    /// </summary>
    public class TestApiClient
    {
        [Fact]
        public async Task TestNetworkError()
        {
            var httpClient = CreateHttpClientThrowing(new HttpRequestException("Connection refused"));

            var apiClient = new ApiClient(httpClient, new Uri("https://localhost:44303/v1/"), "test-key", "test-secret", "en");

            await Assert.ThrowsAsync<HttpRequestException>(async () =>
            {
                await apiClient.VerifySolutionAsync("token", "solution", "127.0.0.1");
            });
        }

        [Fact]
        public async Task TestProblemDetails()
        {
            var httpClient = CreateHttpClientThrowing(
                new ProblemDetailsException(new ProblemDetails()
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    Title = "Invalid token",
                    Status = 400
                }));

            var apiClient = new ApiClient(httpClient, new Uri("https://localhost:44303/v1/"), "test-key", "test-secret", "en");

            await Assert.ThrowsAsync<ProblemDetailsException>(async () =>
            {
                await apiClient.VerifySolutionAsync("bad-token", "solution", "127.0.0.1");
            });
        }

        [Fact]
        public async Task TestSuccess()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"status\":\"success\"}", Encoding.UTF8, "application/json")
            };

            var httpClient = CreateHttpClientReturning(response);

            var apiClient = new ApiClient(httpClient, new Uri("https://localhost:44303/v1/"), "test-key", "test-secret", "en");

            var status = await apiClient.VerifySolutionAsync("token", "solution", "127.0.0.1");

            Assert.Equal(VerifyStatus.Success, status);
        }

        private static HttpClient CreateHttpClientReturning(HttpResponseMessage response)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            return new HttpClient(handlerMock.Object);
        }

        private static HttpClient CreateHttpClientThrowing(Exception ex)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(ex);

            return new HttpClient(handlerMock.Object);
        }
    }
}
