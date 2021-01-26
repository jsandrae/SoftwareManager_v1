using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftwareManager.Shared;

namespace SoftwareManager.Server.Repositories
{
    public interface ISoftwareManagerRepo
    {
        IEnumerable<Software> GetAllSoftware();
        IEnumerable<Software> GetSoftware(string version);
    }
}
