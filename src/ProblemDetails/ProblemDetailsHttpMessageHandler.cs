#region RESTCaptcha API .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    RESTCaptcha API .NET Client
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System.Net.Http.Json;
using System.Net.Mime;

namespace RestCaptcha.Client
{
    /// <summary>
    /// An HTTP message handler for supporting HTTP API responses according to RFC 9457.
    /// </summary>
    public class ProblemDetailsHttpMessageHandler : DelegatingHandler
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ProblemDetailsHttpMessageHandler"/> class.
        /// </summary>
        public ProblemDetailsHttpMessageHandler() 
            : base(new HttpClientHandler()) 
        { 
        }

        /// <summary>
        /// Sends an HTTP request to the inner handler to send to the server. 
        /// </summary>
        /// <param name="request">The HTTP request message</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
        /// contains the HTTP response message.</returns>
        /// <exception cref="ProblemDetailsException"></exception>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var mediaType = response.Content.Headers.ContentType?.MediaType;

                if (mediaType != null && mediaType.StartsWith(MediaTypeNames.Application.ProblemJson, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new ProblemDetailsException(await response.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken));
                }
            }

            return response;
        }
    }
}
