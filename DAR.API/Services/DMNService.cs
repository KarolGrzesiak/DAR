using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DAR.API.Helpers;
using DAR.API.Model.BPM;

namespace DAR.API.Services
{
    public class DMNService : IDMNService
    {
        private readonly DMNModeler _dmnModeler;

        private string _name = "DMN";

        public DMNService(DMNModeler dmnModeler)
        {
            _dmnModeler = dmnModeler ?? throw new System.ArgumentNullException(nameof(dmnModeler));
        }

        public void CreateDMN(ICollection<tTask> createdTasks, IDictionary<string, ICollection<string>> destinationIdToSourceIds)
        {
            var taskToDecisionId = new Dictionary<tTask, string>();
            var taskToOutputId = new Dictionary<tTask, string>();
            var createdBusinessRuleTasks = createdTasks.Where(t => t is tBusinessRuleTask);
            foreach (var task in createdBusinessRuleTasks)
            {

                var decisionId = CreateDecision(task);
                var outputId = CreateOutput(task as tBusinessRuleTask, decisionId);
                taskToDecisionId[task] = decisionId;
                taskToOutputId[task] = outputId;
            }
            foreach (var task in createdBusinessRuleTasks)
            {
                var sourceBusinessRuleTasks = createdTasks.Where(t => destinationIdToSourceIds[task.id].Any(i => i == t.id) && t is tBusinessRuleTask);
                foreach (var sourceTask in sourceBusinessRuleTasks)
                    _dmnModeler.CreateConnection(taskToDecisionId[sourceTask], taskToDecisionId[task]);

            }

            foreach (var (task, decisionId) in taskToDecisionId)
            {
                var sourceTasks = createdTasks.Where(t => destinationIdToSourceIds[task.id].Any(i => i == t.id));
                CreateInput(task, decisionId, sourceTasks, taskToOutputId);
            }
        }

        private void CreateInput(tTask task, string decisionId, IEnumerable<tTask> sourceTasks, IDictionary<tTask, string> taskToOutputId)
        {
            foreach (var sourceTask in sourceTasks)
            {
                if (sourceTask is tUserTask sourceUserTask)
                {
                    var sourceFormField = sourceUserTask.extensionElements.FormData.FormField;
                    _dmnModeler.AddInput(sourceUserTask.name, decisionId, sourceFormField.Type, sourceFormField.Id);
                }
                else
                {
                    var sourceBusinessTask = sourceTask as tBusinessRuleTask;
                    _dmnModeler.AddInput(sourceBusinessTask.name, decisionId, sourceBusinessTask.OutputType, taskToOutputId[sourceTask]);
                }
            }
        }

        private string CreateOutput(tBusinessRuleTask task, string decisionId)
        {
            return _dmnModeler.AddOutput(task.name, decisionId, task.OutputType);

        }

        private string CreateDecision(tTask task)
        {
            var id = _dmnModeler.AddDecision(task.name);
            var businessRuleTask = task as tBusinessRuleTask;
            businessRuleTask.DecisionReference = id;
            return id;
        }



        public void SaveDMN(string filename)
        {
            _dmnModeler.Save(filename);
        }


    }
}