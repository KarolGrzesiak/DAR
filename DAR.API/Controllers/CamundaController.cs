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
        private readonly CamundaSettings _cammundaSettings;
        private readonly IHMLService _hmlService;
        public CamundaController(ICamundaService camundaService, IHMLService hmlService, CamundaSettings cammundaSettings)
        {
            _hmlService = hmlService ?? throw new ArgumentNullException(nameof(_hmlService));
            _cammundaSettings = cammundaSettings ?? throw new System.ArgumentNullException(nameof(cammundaSettings));
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

            var hml = await _hmlService.GetHMLWithARDAsync(id);
            if (hml == null)
                return NotFound();

            _camundaService.Deploy(id, hml.ARD);
            return Created(new Uri(_cammundaSettings.Address), id);

        }
    }
}