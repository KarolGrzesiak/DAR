using System.Collections.Generic;
using System.Threading.Tasks;
using DAR.API.Model;
using Microsoft.AspNetCore.Http;

namespace DAR.API.Services
{
    public interface IHMLService
    {
        Task<bool> DeleteHMLAsync(HML hml);
        Task<bool> CreateHMLsAsync(IEnumerable<HML> hmls);
        Task<IEnumerable<HML>> GetHMLsAsync();
        Task<HML> GetHMLAsync(string id);
        Task<HML> GetHMLWithARDAsync(string id);
        HML ConvertToHML(IFormFile source);
    }
}