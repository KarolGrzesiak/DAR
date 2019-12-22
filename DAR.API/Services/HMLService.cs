using System.IO;
using System.Xml.Serialization;
using DAR.API.Model;
using Microsoft.AspNetCore.Http;

namespace DAR.API.Services
{
    public class HMLService : IHMLService
    {
        public HML ConvertToHML(IFormFile source)
        {
            HML hml = new HML();
            var serializer = new XmlSerializer(typeof(HML));
            using (var stream = source.OpenReadStream())
            {
                hml = (HML)serializer.Deserialize(stream);

            }
            hml.Id = Path.GetFileNameWithoutExtension(source.FileName);
            return hml;
        }
    }
}