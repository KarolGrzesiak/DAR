using System.Collections.Generic;
using System.IO;
using DAR.API.Model.BPM;

namespace DAR.API.Services
{
    public interface IDMNService
    {
        void CreateDMN(ICollection<tTask> createdTasks, IDictionary<string, ICollection<string>> destinationToSourceIds);
        void SaveDMN(string filename);

    }
}