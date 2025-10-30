#region RESTCaptcha - Copyright (C) STÜBER SYSTEMS GmbH
/*    
 *    RESTCaptcha
 *    
 *    Copyright (C) STÜBER SYSTEMS GmbH
 *
 *    This program is free software: you can redistribute it and/or modify
 *    it under the terms of the GNU Affero General Public License, version 3,
 *    as published by the Free Software Foundation.
 *
 *    This program is distributed in the hope that it will be useful,
 *    but WITHOUT ANY WARRANTY; without even the implied warranty of
 *    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *    GNU Affero General Public License for more details.
 *
 *    You should have received a copy of the GNU Affero General Public License
 *    along with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 */
#endregion

using System.Text.Json.Serialization;

namespace RestCaptcha.Client
{
    /// <summary>
    /// Payload of a RESTCaptcha verify response
    /// </summary>
    public class VerifyResponse
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="VerifyResponse"/> class.
        /// </summary>
        /// <param name="status">Verification status value</param>
        /// <param name="hostName">Host name to be verified</param>
        public VerifyResponse(VerifyStatus status, string hostName)
        {
            Status = status;
            HostName = hostName;
        }

        /// <summary>
        /// Verification status indicating whether a submitted token 
        /// and solution were valid.
        /// </summary>
        [JsonPropertyOrder(1)]
        public VerifyStatus Status { get; set; }

        /// <summary>
        /// Host name to be verified
        /// </summary>
        [JsonPropertyOrder(2)]
        public string HostName { get; set; }
    }
}
