using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DAR.Model;
using DAR.Services;
using Microsoft.AspNetCore.Mvc;

namespace DAR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ARDController : ControllerBase
    {
        private string path = @"C:\Users\kgk3\Desktop\Private\Studia\Inżynierka\KEJSY\cases\pli\pli.hml";

        private readonly IDiagramService _diagramService;
        public ARDController(IDiagramService diagramService)
        {
            _diagramService = diagramService ?? throw new ArgumentNullException(nameof(diagramService));

        }

        [HttpGet]
        public async Task<IActionResult> UploadHMLFile([FromBody] hml hmlFile)
        {


            return Ok();
        }



    }
}
