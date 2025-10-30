#region RESTCaptcha API .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    RESTCaptcha API .NET Client
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

namespace RestCaptcha.Client
{
    /// <summary>
    /// A typed HTTP client interface.
    /// </summary>
    public interface IRestClient
    {
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
        public Task<TResult> PostAsync<T, TResult>(Uri requestUrl, T content, CancellationToken cancellationToken)
            where T: class
            where TResult : class;
    }
}
