#region RESTCaptcha API .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    RESTCaptcha API .NET Client
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text.Json;

namespace RestCaptcha.Client
{
    /// <summary>
    /// The implementation of the <see cref="IRestClient"/> interface.
    /// </summary>
    /// <param name="httpClient">An <see cref="HttpClient"/> instance</param>
    public class RestClient(HttpClient httpClient) : IRestClient
    {
        private readonly HttpClient _httpClient = httpClient;

        /// <summary>
        /// Sends an HTTP POST request to the specified API endpoint.
        /// </summary>
        /// <typeparam name="T">The type of the request payload to be sent.</typeparam>
        /// <typeparam name="TResult">The type of the response object expected from the API.</typeparam>
        /// <param name="requestUrl">The full request URL of the target API endpoint.</param>
        /// <param name="content">The request payload to be serialised and sent in the body.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the 
        /// deserialised <typeparamref name="TResult"/> instance returned by the API.</returns>
        public async Task<TResult> PostAsync<T, TResult>(Uri requestUrl, T content, CancellationToken cancellationToken)
            where T : class
            where TResult : class
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);

            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            request.Content = JsonContent.Create(content, null, CreateJsonSerializerOptions());

            var response = await _httpClient.SendAsync(request, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TResult>(CreateJsonSerializerOptions(), cancellationToken);
        }

        /// <summary>
        /// Options to be used with <see cref="JsonSerializer"/>.
        /// </summary>
        /// <returns>A new <see cref="JsonSerializerOptions"/> instance</returns>
        private static JsonSerializerOptions CreateJsonSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }
    }
}