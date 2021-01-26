using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftwareManager.Server.Repositories;

namespace SoftwareManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SoftwareManagerController : ControllerBase
    {
        private readonly ISoftwareManagerRepo _softwareManagerRepo;

        public SoftwareManagerController(ISoftwareManagerRepo softwareManagerRepo)
        {
            _softwareManagerRepo = softwareManagerRepo;
        }

        [HttpGet]
        [Route("GetAllSoftware")]
        public IActionResult GetAllSoftware()
        {
            return Ok(_softwareManagerRepo.GetAllSoftware());
        }

        [HttpGet]
        [Route("GetSoftware/{version}")]
        public IActionResult GetSoftware(string version)
        {
            return Ok(_softwareManagerRepo.GetSoftware(version));
        }
    }
}
