using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DAR.API.Infrastructure;
using DAR.API.Model;
using DAR.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HMLController : ControllerBase
    {
        private readonly DiagramContext _diagramContext;

        private readonly IDiagramService _diagramService;
        public HMLController(DiagramContext context, IDiagramService diagramService)
        {
            _diagramContext = context ?? throw new ArgumentNullException(nameof(context));
            _diagramService = diagramService ?? throw new ArgumentNullException(nameof(diagramService));

        }

        [HttpGet]
        [Route("/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(HML), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<HML>> GetHMLByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            var hml = await _diagramContext.HMLs.SingleOrDefaultAsync(h => h.Id == id);

            if (hml == null)
                return NotFound();

            return hml;

        }
        [HttpGet]
        [ProducesResponseType(typeof(List<HML>), (int)HttpStatusCode.OK)]
        public async Task<List<HML>> GetHMLsAsync()
        {
            var hmls = await _diagramContext.HMLs
                                                .Include(h => h.Properties)
                                                    .ThenInclude(p => p.References)
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
        public async Task<ActionResult> CreateHMLAsync([FromBody] HML hmlFile)
        {
            _diagramContext.Add(hmlFile);
            if (await _diagramContext.SaveChangesAsync() == 0)
                return BadRequest();

            return CreatedAtAction(nameof(GetHMLByIdAsync), new { id = hmlFile.Id }, null);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteHMLAsync(int id)
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
