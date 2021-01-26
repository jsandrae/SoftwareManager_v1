using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using SoftwareManager.Shared;

namespace SoftwareManager.Server.Repositories
{
    public class SoftwareManagerRepo : ISoftwareManagerRepo
    {
        public IEnumerable<Software> GetAllSoftware()
        {
            return new List<Software>
            {
                new Software
                {
                    Name = "MS Word",
                    Version = "13.2.1."
                },
                new Software
                {
                    Name = "AngularJS",
                    Version = "1.7.1"
                },
                new Software
                {
                    Name = "Angular",
                    Version = "8.1.13"
                },
                new Software
                {
                    Name = "React",
                    Version = "0.0.5"
                },
                new Software
                {
                    Name = "Vue.js",
                    Version = "2.6"
                },
                new Software
                {
                    Name = "Visual Studio",
                    Version = "2017.0.1"
                },
                new Software
                {
                    Name = "Visual Studio",
                    Version = "2019.1"
                },
                new Software
                {
                    Name = "Visual Studio Code",
                    Version = "1.35"
                },
                new Software
                {
                    Name = "Blazor",
                    Version = "0.7"
                }
            };
        }

        public IEnumerable<Software> GetSoftware(string stringVersion)
        {
            var requestedVersion = GetVersionFromString(stringVersion);

            if (requestedVersion == null)
                return null;

            // Create empty list to store software that meets criteria
            var requestedSoftwareList = new List<Software>();

            // Get list of all software
            var fullSoftwareList = GetAllSoftware();

            foreach (var software in fullSoftwareList)
            {
                var softwareVersion = GetVersionFromString(software.Version);
                // Compare versions
                if (requestedVersion.Major != softwareVersion.Major)
                {
                    // Definite comparison reached, add if appropriate 
                    if (requestedVersion.Major < softwareVersion.Major)
                        requestedSoftwareList.Add(software);
                    continue;
                }
                if (requestedVersion.Minor != softwareVersion.Minor)
                {
                    // Definite comparison reached, add if appropriate 
                    if (requestedVersion.Minor < softwareVersion.Minor)
                        requestedSoftwareList.Add(software);
                    continue;
                }
                if (requestedVersion.Patch < softwareVersion.Patch)
                    requestedSoftwareList.Add(software);
            }

            return requestedSoftwareList;
        }

        private VersionObject GetVersionFromString(string stringVersion)
        {
            if (string.IsNullOrEmpty(stringVersion))
            {
                Log.Error($"Error SoftwareManagerRepo.GetVersionFromString: no version given.");
                return null;
            }

            // Split string on delimiter: .
            var subversions = stringVersion.Split('.', StringSplitOptions.RemoveEmptyEntries);

            if (subversions.Length > 3)
            {
                Log.Error($"Error SoftwareManagerRepo.GetVersionFromString: invalid version given {stringVersion}.");
                return null;
            }

            // Create an array to hold version integers 
            int[] versionInt = {0, 0, 0};
            var i = 0;
            
            foreach (var subversion in subversions)
            {
                var couldParse = short.TryParse(subversion, out var intResult);
                if (!couldParse)
                {
                    var message =
                        $"Error SoftwareManagerRepo.GetVersionFromString: Could not get version from {stringVersion}.";
                    Log.Error(message);
                    return null;
                }

                versionInt[i++] = intResult;
            }
            
            var major = versionInt[0];
            var minor = versionInt[1];
            var patch = versionInt[2];

            return new VersionObject { Major = major, Minor = minor, Patch = patch };
        }
    }
}
