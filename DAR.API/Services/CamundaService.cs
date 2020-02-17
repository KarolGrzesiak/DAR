using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using CamundaClient;
using CamundaClient.Dto;
using DAR.API.Extensions;
using DAR.API.Helpers;
using DAR.API.Model;
using DAR.API.Model.BPM;

namespace DAR.API.Services
{
    public class CamundaService : ICamundaService
    {
        private readonly CamundaEngineClient _camunda;
        private readonly IBPMService _bpmService;
        private readonly IDMNService _dmnService;

        public CamundaService(CamundaEngineClient camunda, IBPMService bpmService, IDMNService dmnService)
        {
            _dmnService = dmnService ?? throw new ArgumentNullException(nameof(dmnService));
            _camunda = camunda ?? throw new System.ArgumentNullException(nameof(camunda));
            _bpmService = bpmService ?? throw new ArgumentNullException(nameof(bpmService));
        }
        public void Deploy(string name, IEnumerable<Dependency> ard)
        {
            CreateModels(name, ard);


            var filesToDeploy = PrepareFiles(name);

            _camunda.RepositoryService.Deploy(Guid.NewGuid().ToString(), filesToDeploy);

        }
        private List<object> PrepareFiles(string name)
        {
            var files = new List<object>();
            for (int i = 0; i < 2; i++)
            {
                var fileExtension = i == 0 ? ".bpmn" : ".dmn";
                files.Add(new FileParameter(File.ReadAllBytes(name + fileExtension), name + fileExtension));
                File.Delete(name + fileExtension);
            }
            return files;
        }

        private void CreateModels(string id, IEnumerable<Dependency> ard)
        {
            _bpmService.CreateBPM(id, ard);
            _dmnService.CreateDMN(_bpmService.CreatedTasks, _bpmService.DestinationIdToSourcesIds);
            _bpmService.SaveBPM(id);
            _dmnService.SaveDMN(id);
        }
    }
}