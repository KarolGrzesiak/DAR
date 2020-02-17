using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DAR.API.Infrastructure;
using DAR.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DAR.API.Services
{
    public class HMLService : IHMLService
    {
        private readonly DiagramContext _context;
        public HMLService(DiagramContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

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

        public async Task<HML> GetHMLWithARDAsync(string id)
        {
            return await _context.HMLs.Include(h => h.ARD)
                                                    .ThenInclude(a => a.DestinationProperty)
                                                        .ThenInclude(p => p.References)
                                                            .ThenInclude(r => r.Attribute)
                                                .Include(h => h.ARD)
                                                    .ThenInclude(a => a.SourceProperty)
                                                        .ThenInclude(p => p.References)
                                                            .ThenInclude(r => r.Attribute)
                                                .SingleOrDefaultAsync(h => h.Id == id);
        }
        public async Task<HML> GetHMLAsync(string id)
        {
            return await _context.HMLs.Include(h => h.ARD)
                                        .ThenInclude(a => a.DestinationProperty)
                                            .ThenInclude(p => p.References)
                                                .ThenInclude(r => r.Attribute)
                                    .Include(h => h.ARD)
                                        .ThenInclude(a => a.SourceProperty)
                                            .ThenInclude(p => p.References)
                                                .ThenInclude(r => r.Attribute)
                                    .Include(h => h.Types)
                                        .ThenInclude(t => t.Domain)
                                            .ThenInclude(d => d.Values)
                                    .SingleOrDefaultAsync(h => h.Id == id);
        }
        public async Task<IEnumerable<HML>> GetHMLsAsync()
        {
            return await _context.HMLs.Include(h => h.Properties)
                                                .Include(h => h.TPH)
                                                .Include(h => h.ARD)
                                                .Include(h => h.Attributes)
                                                .Include(h => h.Types)
                                                .ToListAsync();
        }
        public async Task<bool> CreateHMLsAsync(IEnumerable<HML> hmls)
        {
            _context.AddRange(hmls);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<bool> DeleteHMLAsync(HML hml)
        {
            _context.Remove(hml);
            return await _context.SaveChangesAsync() != 0;
        }
    }
}