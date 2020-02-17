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
        private readonly IHMLService _hmlService;

        public HMLController(IHMLService hmlService)
        {
            _hmlService = hmlService ?? throw new ArgumentNullException(nameof(hmlService));

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

            var hml = await _hmlService.GetHMLAsync(id);

            if (hml == null)
                return NotFound();

            return Ok(hml);

        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HML>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<HML>>> GetHMLsAsync()
        {
            var hmls = await _hmlService.GetHMLsAsync();
            return Ok(hmls);
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateHMLsAsync(IFormCollection files)
        {
            var hmls = new List<HML>();
            foreach (var file in files.Files)
            {
                hmls.Add(_hmlService.ConvertToHML(file));
            }

            if (!await _hmlService.CreateHMLsAsync(hmls))
                return BadRequest();

            return CreatedAtAction(nameof(GetHMLsAsync), null);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteHMLAsync(string id)
        {
            var hml = await _hmlService.GetHMLAsync(id);
            if (hml == null)
                return NotFound();

            if (!await _hmlService.DeleteHMLAsync(hml))
                return BadRequest();
            return NoContent();
        }



    }
}
