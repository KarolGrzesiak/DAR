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
        private readonly IBPMService _bpmService;
        private readonly IHMLService _hmlService;

        public BPMController(IBPMService bpmService, IHMLService hmlService)
        {
            _hmlService = hmlService ?? throw new System.ArgumentNullException(nameof(hmlService));
            _bpmService = bpmService ?? throw new System.ArgumentNullException(nameof(bpmService));
        }

        [HttpPost]
        [Route("{id}")]
        [ProducesResponseType(typeof(FileStreamResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetBPMModelAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var hml = await _hmlService.GetHMLWithARDAsync(id);
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