using System.Collections;
using System.Collections.Generic;
using DAR.API.Model;

namespace DAR.API.Services
{
    public interface IBPMService
    {
        void CreateBPM(string filename, IEnumerable<Dependency> ard);
    }
}