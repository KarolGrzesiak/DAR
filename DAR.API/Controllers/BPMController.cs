using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DAR.API.Infrastructure;
using DAR.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAR.API.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class BPMController : ControllerBase
    {
        private readonly DiagramContext _diagramContext;
        private readonly IBPMService _bpmService;
        private readonly IDMNService _dmnService;

        public BPMController(DiagramContext diagramContext, IBPMService bpmService, IDMNService dmnService)
        {
            _diagramContext = diagramContext ?? throw new System.ArgumentNullException(nameof(diagramContext));
            _bpmService = bpmService ?? throw new System.ArgumentNullException(nameof(bpmService));
            _dmnService = dmnService ?? throw new System.ArgumentNullException(nameof(dmnService));
        }

        [HttpPost]
        [Route("{id}")]
        [ProducesResponseType(typeof(FileStreamResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetBPMModelAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var hml = await _diagramContext.HMLs.Include(h => h.ARD)
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
            if (hml == null)
                return NotFound();

            _bpmService.CreateBPM(id, hml.ARD);
            _bpmService.SaveBPM(id);
            var data = System.IO.File.ReadAllBytes(id + ".bpmn");
            System.IO.File.Delete(id + ".bpmn");

            return File(data, "application/octet-stream");
        }

    }


}