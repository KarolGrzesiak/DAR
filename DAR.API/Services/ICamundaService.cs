using System.Collections.Generic;
using System.IO;
using DAR.API.Model;

namespace DAR.API.Services
{
    public interface ICamundaService
    {
        void Deploy(string name, IEnumerable<Dependency> ard);
    }
}