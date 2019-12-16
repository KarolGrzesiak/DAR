using DAR.API.Model;
using Microsoft.AspNetCore.Http;

namespace DAR.API.Services
{
    public interface IHMLService
    {
        HML ConvertToHML(IFormFile source);
    }
}