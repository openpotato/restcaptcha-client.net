#region RESTCaptcha API .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    RESTCaptcha API .NET Client
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System.Text.Json.Serialization;

namespace RestCaptcha.Client
{
    /// <summary>
    /// A machine-readable format for specifying errors in HTTP API responses based on 
    /// <see href="https://datatracker.ietf.org/doc/html/rfc9457"/>.
    /// </summary>
    public class ProblemDetails
    {
        /// <summary>
        /// A detailed, human-readable explanation of the error.
        /// </summary>
        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        /// <summary>
        /// A grouped list of validation errors
        /// </summary>
        [JsonPropertyName("errors")]
        public Dictionary<string, string[]> Errors { get; set; }

        /// <summary>
        /// An URI pointing to the specific instance of the error.
        /// </summary>
        [JsonPropertyName("instance")]
        public string Instance { get; set; }

        /// <summary>
        /// The HTTP status code 
        /// </summary>
        [JsonPropertyName("status")]
        public int? Status { get; set; }

        /// <summary>
        /// A short, human-readable summary of the problem type. 
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// An unique identifier used for correlation and troubleshooting in distributed systems
        /// </summary>
        [JsonPropertyName("traceId")]
        public string TraceId { get; set; }

        /// <summary>
        /// An URI reference that identifies the problem type. 
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
