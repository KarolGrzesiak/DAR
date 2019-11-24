using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using DAR.API.Constants;
using DAR.API.Extensions;
using DAR.API.Helpers;
using DAR.API.Model;

namespace DAR.API.Services
{
    public class BPMService : IBPMService
    {
        private IDictionary<string, ICollection<string>> _destinationIdToSourcesIds;
        private ICollection<tEvent> _createdEvents;
        private ICollection<BPMNShape> _createdObjects;
        private ICollection<tTask> _createdTasks;
        private readonly BPMModeler _modeler;
        private double _numberForId;
        public BPMService(BPMModeler modeler)
        {
            _modeler = modeler;
            _destinationIdToSourcesIds = new Dictionary<string, ICollection<string>>();
            _createdTasks = new Collection<tTask>();
            _createdEvents = new Collection<tEvent>();
            _createdObjects = new Collection<BPMNShape>();
        }

        public void CreateBPM(string filename, IEnumerable<Dependency> ard)
        {
            CreateTasks(ard);
            CreateEvents(ard);
            ConnectEventsAndTasks(ard);


            _modeler.Save(filename);

        }
        private string CreateUselessId(string from)
        {
            return from + _numberForId++.ToString();
        }

        private void ConnectEventsAndTasks(IEnumerable<Dependency> ard)
        {
            var startTasks = _createdTasks.Where(t => t is tUserTask);

            var startEvent = _createdEvents.SingleOrDefault(e => e is tStartEvent);
            var endEvent = _createdEvents.SingleOrDefault(e => e is tEndEvent);
            ConnectStartEventWithTasks(startEvent, startTasks);
            var lastTask = ConnectTasksRecursive(startTasks);
            ConnectEndEventWithTask(endEvent, lastTask);
        }

        private void ConnectEndEventWithTask(tEvent endEvent, tTask lastTask)
        {
            var lastObject = _createdObjects.Single(o => o.bpmnElement.ToString() == lastTask.id);
            var endEventObject = _modeler.CreateObject(CreateUselessId("eventObjectId"), endEvent.id, BPMConstants.EventWidth, BPMConstants.EventHeight, lastObject.Bounds.x + BPMConstants.HorizontalSpaceBetweenObjects, lastObject.Bounds.y);
            _modeler.CreateFlow(CreateUselessId("flowId"), lastObject, endEventObject);
        }

        private tTask ConnectTasksRecursive(IEnumerable<tTask> tasksToConnect)
        {
            var newTasksToConnect = new Collection<tTask>();
            foreach (var task in tasksToConnect)
            {
                var destinationToSourcesId = _destinationIdToSourcesIds.SingleOrDefault(vk => vk.Value.Any(id => id == task.id));
                if (_createdObjects.Any(o => o.bpmnElement.ToString() == destinationToSourcesId.Key))
                    continue;
                var sourceObjects = _createdObjects.Where(o => destinationToSourcesId.Value.Contains(o.bpmnElement.ToString()));
                if (sourceObjects.Count() != destinationToSourcesId.Value.Count())
                {
                    var notConnectedIds = destinationToSourcesId.Value.Where(s => !sourceObjects.Any(o => o.bpmnElement.ToString() == s));
                    foreach (var id in notConnectedIds)
                    {
                        ConnectTasksRecursive(_createdTasks.Where(t => _destinationIdToSourcesIds[id].Any(s => s == t.id)));
                    }

                    sourceObjects = _createdObjects.Where(o => destinationToSourcesId.Value.Contains(o.bpmnElement.ToString()));
                }
                if (sourceObjects.Count() > 1)
                {
                    var gatewayId = CreateUselessId("gatewayId");
                    var gateway = _modeler.CreateGateway(gatewayId, GatewayType.Parallel);
                    var sourceObjectsBounds = sourceObjects.Select(o => o.Bounds);
                    var gatewayY = sourceObjectsBounds.Select(b => b.y).Min() + ((sourceObjectsBounds.Select(b => b.y).Max() - sourceObjectsBounds.Select(b => b.y).Min()) / 2);
                    var gatewayX = sourceObjectsBounds.Select(b => b.x).Max() + BPMConstants.HorizontalSpaceBetweenObjects;
                    var gatewayObject = _modeler.CreateObject(CreateUselessId("gatewayObjectId"), gatewayId, BPMConstants.GatewayWidth, BPMConstants.GatewayHeight, gatewayX, gatewayY);
                    foreach (var bpmObject in sourceObjects)
                    {
                        _modeler.CreateFlow(CreateUselessId("flowId"), bpmObject, gatewayObject);
                    }
                    var destinationObject = _modeler.CreateObject(CreateUselessId("taskObjectId"), destinationToSourcesId.Key, BPMConstants.TaskWidth, BPMConstants.TaskHeight, gatewayX + BPMConstants.HorizontalSpaceBetweenObjects, gatewayY);
                    _modeler.CreateFlow(CreateUselessId("flowId"), gatewayObject, destinationObject);
                    _createdObjects.Add(destinationObject);

                }
                else
                {
                    var sourceObject = sourceObjects.FirstOrDefault();
                    var destinationObject = _modeler.CreateObject(CreateUselessId("taskObjectId"), destinationToSourcesId.Key, BPMConstants.TaskWidth, BPMConstants.TaskHeight, sourceObject.Bounds.x + BPMConstants.HorizontalSpaceBetweenObjects, sourceObject.Bounds.y);
                    _modeler.CreateFlow(CreateUselessId("flowId"), sourceObject, destinationObject);
                    _createdObjects.Add(destinationObject);
                }
                newTasksToConnect.Add(_createdTasks.Single(t => t.id == destinationToSourcesId.Key));
            }
            if (newTasksToConnect.Count() > 1)
                return ConnectTasksRecursive(newTasksToConnect);
            else
                return newTasksToConnect.First();

        }

        private void ConnectStartEventWithTasks(tEvent startEvent, IEnumerable<tTask> startTasks)
        {
            if (startTasks.Count() > 1)
            {
                double y = 0;
                double x = 0;
                var gatewayId = CreateUselessId("gatewayId");
                var gateway = _modeler.CreateGateway(gatewayId, GatewayType.Parallel);
                var gatewayY = startTasks.Count() / 2 * BPMConstants.VerticalSpaceBetweenObjects + BPMConstants.GatewayHeight;
                var startEventObject = _modeler.CreateObject(CreateUselessId("eventObjectId"), startEvent.id, BPMConstants.EventWidth, BPMConstants.EventHeight, BPMConstants.EventWidth, gatewayY);
                _createdObjects.Add(startEventObject);
                x += BPMConstants.HorizontalSpaceBetweenObjects;
                var gatewayObject = _modeler.CreateObject(CreateUselessId("gatewayObjectId"), gatewayId, BPMConstants.GatewayWidth, BPMConstants.GatewayHeight, x, gatewayY);
                _createdObjects.Add(gatewayObject);
                _modeler.CreateFlow(CreateUselessId("flowId"), startEventObject, gatewayObject);
                x += BPMConstants.HorizontalSpaceBetweenObjects;
                foreach (var task in startTasks)
                {
                    var taskObject = _modeler.CreateObject(CreateUselessId("taskObjectId"), task.id, BPMConstants.TaskWidth, BPMConstants.TaskHeight, x, y + BPMConstants.TaskHeight);
                    _createdObjects.Add(taskObject);
                    y += BPMConstants.VerticalSpaceBetweenObjects;
                    _modeler.CreateFlow(CreateUselessId("flowId"), gatewayObject, taskObject);
                }

            }
            else
            {
                var startEventObject = _modeler.CreateObject(CreateUselessId("eventObjectId"), startEvent.id, BPMConstants.EventWidth, BPMConstants.EventHeight, BPMConstants.EventWidth, BPMConstants.EventHeight);
                _createdObjects.Add(startEventObject);
                var taskObject = _modeler.CreateObject(CreateUselessId("taskObjectId"), startTasks.SingleOrDefault().id, BPMConstants.TaskWidth, BPMConstants.TaskHeight, BPMConstants.HorizontalSpaceBetweenObjects, BPMConstants.TaskHeight);
                _createdObjects.Add(taskObject);
                _modeler.CreateFlow(CreateUselessId("flowId"), startEventObject, taskObject);
            }
        }

        private void CreateEvents(IEnumerable<Dependency> ard)
        {
            _createdEvents.Add(_modeler.CreateEvent(CreateUselessId("startEventId"), EventType.Start));
            _createdEvents.Add(_modeler.CreateEvent(CreateUselessId("endEventId"), EventType.End));
        }




        private void CreateTasks(IEnumerable<Dependency> ard)
        {
            var destinationsProperties = ard.Select(a => a.DestinationProperty);
            foreach (var connection in ard)
            {
                var taskType = TaskType.BusinessRule;
                string taskName;
                if (!_createdTasks.Any(t => t.id == connection.Destination))
                {
                    var words = Regex.Matches(connection.DestinationProperty.References.SingleOrDefault().Attribute.Name, @"(^[a-z]+|[A-Z]+(?![a-z])|[A-Z][a-z]+)")
                                            .Cast<Match>()
                                            .Select(m => m.Value.FirstCharToUpper());
                    taskName = string.Join(" ", words);
                    var task = _modeler.CreateTask(connection.Destination, taskName, taskType);
                    _createdTasks.Add(task);

                }
                if (!_createdTasks.Any(t => t.id == connection.Source))
                {
                    taskType = destinationsProperties.Any(p => p == connection.SourceProperty) ? TaskType.BusinessRule : TaskType.User;
                    var words = Regex.Matches(connection.SourceProperty.References.SingleOrDefault().Attribute.Name, @"(^[a-z]+|[A-Z]+(?![a-z])|[A-Z][a-z]+)")
                                              .Cast<Match>()
                                              .Select(m => m.Value.FirstCharToUpper());
                    taskName = string.Join(" ", words);
                    var task = _modeler.CreateTask(connection.Source, taskName, taskType);
                    _createdTasks.Add(task);
                }
                AddToDestinationSourcesDictionary(connection.Destination, connection.Source);
            }
        }

        private void AddToDestinationSourcesDictionary(string destination, string source)
        {
            if (_destinationIdToSourcesIds.TryGetValue(destination, out var sources))
                sources.Add(source);
            else
            {
                sources = new Collection<string>{
                        source
                    };
                _destinationIdToSourcesIds.Add(destination, sources);
            }
        }


    }
}