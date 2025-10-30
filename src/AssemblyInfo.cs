#region RESTCaptcha API .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    RESTCaptcha API .NET Client
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System.Reflection;

namespace RestCaptcha.Client
{
    /// <summary>
    /// Helper class for retrieving assembly information.
    /// </summary>
    public static class AssemblyInfo
    {
        /// <summary>
        /// Gets the name of the currently executing assembly.
        /// </summary>
        /// <returns>The simple (unqualified) name of the executing assembly.</returns>
        public static string GetAgentName()
        {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }

        /// <summary>
        /// Gets the version of the currently executing assembly.
        /// </summary>
        /// <returns>The version number of the executing assembly as a string</returns>
        public static string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}