using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DAR.API.Infrastructure;
using DAR.API.Model;
using DAR.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAR.API.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class HMLController : ControllerBase
    {
        private readonly DiagramContext _diagramContext;
        private readonly IHMLService _hmlService;

        public HMLController(DiagramContext context, IHMLService hmlService)
        {
            _hmlService = hmlService;
            _diagramContext = context ?? throw new ArgumentNullException(nameof(context));

        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(HML), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<HML>> GetHMLByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var hml = await _diagramContext.HMLs.Include(h => h.Properties)

                                                .Include(h => h.TPH)
                                                .Include(h => h.ARD)
                                                .Include(h => h.Attributes)
                                                .Include(h => h.Types)
                                                    .ThenInclude(t => t.Domain)
                                                        .ThenInclude(d => d.Values)
                                                .SingleOrDefaultAsync(h => h.Id == id);

            if (hml == null)
                return NotFound();

            return hml;

        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HML>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<HML>>> GetHMLsAsync()
        {
            var hmls = await _diagramContext.HMLs.Include(h => h.Properties)
                                                .Include(h => h.TPH)
                                                .Include(h => h.ARD)
                                                .Include(h => h.Attributes)
                                                .Include(h => h.Types)
                                                    .ThenInclude(t => t.Domain)
                                                        .ThenInclude(d => d.Values)
                                                .ToListAsync();
            return hmls;
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateHMLsAsync(IFormCollection files)
        {
            _diagramContext.Add(_hmlService.ConvertToHML(files.Files[0]));

            if (await _diagramContext.SaveChangesAsync() == 0)
                return BadRequest();

            return CreatedAtAction(nameof(GetHMLByIdAsync), new { id = files.Files[0].FileName }, null);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteHMLAsync(string id)
        {
            var hml = _diagramContext.HMLs.SingleOrDefault(h => h.Id == id);
            if (hml == null)
                return NotFound();
            _diagramContext.Remove(hml);
            if (await _diagramContext.SaveChangesAsync() == 0)
                return BadRequest();
            return NoContent();
        }



    }
}
