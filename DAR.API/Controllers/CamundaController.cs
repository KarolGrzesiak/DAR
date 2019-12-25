using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DAR.API.Infrastructure;
using DAR.API.Infrastructure.Settings;
using DAR.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CamundaController : ControllerBase
    {
        private readonly ICamundaService _camundaService;
        private readonly IBPMService _bpmService;
        private readonly DiagramContext _diagramContext;
        private readonly IDMNService _dmnService;
        private readonly CamundaSettings _cammundaSettings;
        public CamundaController(DiagramContext diagramContext, ICamundaService camundaService, IBPMService bpmService, IDMNService dmnService, CamundaSettings cammundaSettings)
        {
            _cammundaSettings = cammundaSettings ?? throw new System.ArgumentNullException(nameof(cammundaSettings));
            _dmnService = dmnService ?? throw new System.ArgumentNullException(nameof(dmnService));
            _diagramContext = diagramContext ?? throw new System.ArgumentNullException(nameof(diagramContext));
            _bpmService = bpmService ?? throw new System.ArgumentNullException(nameof(bpmService));
            _camundaService = camundaService ?? throw new System.ArgumentNullException(nameof(camundaService));
        }

        [HttpPost]
        [Route("deploy/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Uri), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> DeployAsync(string id)
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
            _dmnService.CreateDMN(_bpmService.CreatedTasks, _bpmService.DestinationIdToSourcesIds);
            _bpmService.SaveBPM(id);
            _dmnService.SaveDMN(id);

            _camundaService.Deploy(id);
            return Created(new Uri(_cammundaSettings.Address), id);

        }
    }
}