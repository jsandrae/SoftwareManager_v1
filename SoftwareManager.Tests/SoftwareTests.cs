using System;
using System.Collections.Generic;
using System.Text;
using SoftwareManager.Server.Repositories;
using SoftwareManager.Shared;
using Xunit;
using System.Linq;

namespace SoftwareManager.Tests
{
    public class SoftwareTests
    {
        private readonly SoftwareManagerRepo _softwareManagerRepo;

        public SoftwareTests()
        {
            _softwareManagerRepo = new SoftwareManagerRepo();
        }

        [Fact]
        public void ShouldReturnNullWithoutVersion()
        {
            var emptyResponse = _softwareManagerRepo.GetSoftware("");
            var nullResponse = _softwareManagerRepo.GetSoftware(null);

            Assert.Null(emptyResponse);
            Assert.Null(nullResponse);
        }

        [Theory]
        [InlineData("13.12.11.10")]
        [InlineData("13.12.11.10.9")]
        [InlineData("13.12.11.10.9.8.7.6")]
        public void ShouldReturnNullWithInvalidVersions(string versions)
        {
            Assert.Null(_softwareManagerRepo.GetSoftware(versions));
        }

        [Fact]
        public void ShouldReturnSoftwareWithGivenVersion()
        {
            const string version = "1";

            var response = _softwareManagerRepo.GetSoftware(version);

            Assert.NotNull(response);
        }

        [Fact]
        public void ShouldReturnSingletonSoftwareWithMaxVersion()
        {
            var software = new Software
            {
                Name = "Visual Studio",
                Version = "2019.1"
            };

            const string requestedVersion = "2019.0.1";

            var response = _softwareManagerRepo.GetSoftware(requestedVersion);

            Assert.NotNull(response);
            var softwareList = response.ToList();
            Assert.Single(softwareList);
            Assert.Equal(software.Name, softwareList.First().Name);
            Assert.Equal(software.Version, softwareList.First().Version);
        }
    }
}
