using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using CamundaClient;
using CamundaClient.Dto;
using DAR.API.Extensions;
using DAR.API.Helpers;
using DAR.API.Model.BPM;

namespace DAR.API.Services
{
    public class CamundaService : ICamundaService
    {
        private readonly CamundaEngineClient _camunda;
        public CamundaService(CamundaEngineClient camunda)
        {
            _camunda = camunda ?? throw new System.ArgumentNullException(nameof(camunda));
        }
        public void Deploy(string name)
        {
            var files = new List<object>();
            for (int i = 0; i < 2; i++)
            {
                var fileExtension = i == 0 ? ".bpmn" : ".dmn";
                files.Add(new FileParameter(File.ReadAllBytes(name + fileExtension), name + fileExtension));
                File.Delete(name + fileExtension);
            }



            _camunda.RepositoryService.Deploy(Guid.NewGuid().ToString(), files);

        }
    }
}