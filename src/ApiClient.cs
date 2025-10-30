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
    /// Client for for interacting with the RESTCaptcha API.
    /// </summary>
    public class ApiClient
    {
        private readonly Uri _baseUrl;
        private readonly string _language;
        private readonly IRestClient _restClient;
        private readonly string _siteKey;
        private readonly string _siteSecret;

        /// <summary>
        /// Initialises a new instance of the <see cref="ApiClient"/> class.
        /// </summary>
        /// <param name="httpClient">A <see cref="HttpClient"/> instance</param>
        /// <param name="baseUrl">The base url of the RESTCaptcha API</param>
        /// <param name="siteKey">The public site key used to identify the client with the RESTCaptcha service.</param>
        /// <param name="siteSecret">The private site secret used to authorize verification requests.</param>
        /// <param name="language">The language code for message translation.</param>
        public ApiClient(HttpClient httpClient, Uri baseUrl, string siteKey, string siteSecret, string language = null)
        {
            _restClient = new RestClient(httpClient);
            _baseUrl = baseUrl;
            _siteKey = siteKey;
            _siteSecret = siteSecret;
            _language = language;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ApiClient"/> class.
        /// </summary>
        /// <param name="restClient">An implementation of <see cref="IRestClient"/></param>
        /// <param name="baseUrl">The base url of the RESTCaptcha API</param>
        /// <param name="siteKey">The public site key used to identify the client with the RESTCaptcha service.</param>
        /// <param name="siteSecret">The private site secret used to authorize verification requests.</param>
        /// <param name="language">The language code for message translation.</param>
        public ApiClient(IRestClient restClient, Uri baseUrl, string siteKey, string siteSecret, string language = null)
        {
            _restClient = restClient;
            _baseUrl = baseUrl;
            _siteKey = siteKey;
            _siteSecret = siteSecret;
            _language = language;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ApiClient"/> class.
        /// </summary>
        /// <param name="baseUrl">The base url of the RESTCaptcha API</param>
        /// <param name="siteKey">The public site key used to identify the client with the RESTCaptcha service.</param>
        /// <param name="siteSecret">The private site secret used to authorize verification requests.</param>
        /// <param name="language">The language code for message translation.</param>
        public ApiClient(Uri baseUrl, string siteKey, string siteSecret, string language = null)
            : this(RestClientFactory.CreateRestClient(), baseUrl, siteKey, siteSecret, language)
        {
        }

        /// <summary>
        /// Verifies a RESTCaptcha solution with the server and returns the verification status.
        /// </summary>
        /// <param name="token">The RESTCaptcha token received from the widget.</param>
        /// <param name="solution">The user-submitted solution to the challenge.</param>
        /// <param name="callerIp">The IP address of the backend submitting the solution.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task<VerifyStatus> VerifySolutionAsync(string token, string solution, string callerIp = null, CancellationToken cancellationToken = default)
        {
            var response = await GetRestClient().PostAsync<VerifyRequest, VerifyResponse>(
                CreateUriBuilder()
                    .WithRelativePath("verify")
                    .WithParameter("siteKey", _siteKey)
                    .WithParameter("language", _language)
                    .Uri,
                new VerifyRequest() { SiteSecret = _siteSecret, Token = token, Solution = solution, CallerIp = callerIp }, 
                cancellationToken);
            return response.Status;
        }

        /// <summary>
        /// Creates an uri builder with the internal base url as strating point
        /// </summary>
        /// <returns>A new <see cref="UriBuilder"/> instance</returns>
        protected UriBuilder CreateUriBuilder()
        {
            return new UriBuilder(_baseUrl);
        }

        /// <summary>
        /// Gives back the interanal instance of the <see cref="IRestClient"/> implmentation
        /// </summary>
        /// <returns>A new <see cref="IRestClient"/> implementation</returns>
        protected IRestClient GetRestClient()
        {
            return _restClient;
        }
    }
}
