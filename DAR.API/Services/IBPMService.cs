using System.Collections;
using System.Collections.Generic;
using System.IO;
using DAR.API.Model;
using DAR.API.Model.BPM;

namespace DAR.API.Services
{
    public interface IBPMService
    {
        ICollection<tTask> CreatedTasks { get; set; }
        IDictionary<string, ICollection<string>> DestinationIdToSourcesIds { get; set; }
        void CreateBPM(string id, IEnumerable<Dependency> ard);
        void SaveBPM(string filename);
    }
}