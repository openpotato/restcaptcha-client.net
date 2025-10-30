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
    /// Payload of a RESTCaptcha verify request
    /// </summary>
    public class VerifyRequest
    {
        /// <summary>
        /// The IP address of the backend submitting the solution.
        /// </summary>
        public string CallerIp { get; set; }

        /// <summary>
        /// The private site secret used to authorize verification requests.
        /// </summary>
        public string SiteSecret { get; set; }

        /// <summary>
        /// The user-submitted solution to the challenge.
        /// </summary>
        public string Solution { get; set; }

        /// <summary>
        /// The RESTCaptcha token received from the widget.
        /// </summary>
        public string Token { get; set; }
    }
}
