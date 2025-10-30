#region RESTCaptcha API .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    RESTCaptcha API .NET Client
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System.Text;

namespace RestCaptcha.Client
{
    /// <summary>
    /// An exception for <see cref="ProblemDetails"/> based API errors.
    /// </summary>
    public class ProblemDetailsException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ProblemDetailsException"/> class.
        /// </summary>
        /// <param name="details">The details object according to RFC 9457</param>
        public ProblemDetailsException(ProblemDetails details)
        {
            Details = details;
        }

        /// <summary>
        /// The details object according to RFC 9457
        /// </summary>
        public ProblemDetails Details { get; }

        /// <summary>
        /// String representation of the object instance
        /// </summary>
        /// <returns>The string representation </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Type    : {Details.Type}");
            sb.AppendLine($"Title   : {Details.Title}");
            sb.AppendLine($"Status  : {Details.Status}");
            sb.AppendLine($"Detail  : {Details.Detail}");
            sb.AppendLine($"Instance: {Details.Instance}");
            sb.AppendLine($"Errors  : {Details.Errors?.ToString()}");
            sb.AppendLine($"TraceId : {Details.TraceId}");

            return sb.ToString();
        }
    }
}
