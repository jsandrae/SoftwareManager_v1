using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareManager.Shared
{
    public class Software
    {
        /// <summary>
        /// Name of software
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Version of software in format [major version].[minor version].[patch]
        /// </summary>
        public string Version { get; set; }
    }
}
