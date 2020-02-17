using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using CamundaClient;
using CamundaClient.Dto;
using DAR.API.Constants;
using DAR.API.Model.Enums;
using DAR.API.Model.BPM;
using DAR.API.Extensions;
using DAR.API.Helpers;
using DAR.API.Model;

namespace DAR.API.Services
{
    public class BPMService : IBPMService
    {
        private IDictionary<string, ICollection<string>> _destinationIdToSourcesIds;
        private readonly ICollection<tEvent> _createdEvents;
        private readonly ICollection<BPMNShape> _createdObjects;
        private ICollection<tTask> _createdTasks;
        private readonly BPMModeler _bpmModeler;
        private double _numberForId;

        public ICollection<tTask> CreatedTasks { get => _createdTasks; set => _createdTasks = value; }
        public IDictionary<string, ICollection<string>> DestinationIdToSourcesIds { get => _destinationIdToSourcesIds; set => _destinationIdToSourcesIds = value; }

        public BPMService(BPMModeler bpmModeler)
        {
            _bpmModeler = bpmModeler ?? throw new ArgumentNullException(nameof(bpmModeler));
            _destinationIdToSourcesIds = new Dictionary<string, ICollection<string>>();
            _createdTasks = new Collection<tTask>();
            _createdEvents = new Collection<tEvent>();
            _createdObjects = new Collection<BPMNShape>();

        }

        public void CreateBPM(string id, IEnumerable<Dependency> ard)
        {
            _bpmModeler.ProcessId = id;
            CreateTasks(ard);
            CreateEvents();
            ConnectEventsAndTasks();
        }

        public void SaveBPM(string filename)
        {

            _bpmModeler.Save(filename);


        }
        private string CreateUselessId(string from)
        {
            return from + _numberForId++.ToString();
        }

        private void ConnectEventsAndTasks()
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
            var endEventObject = _bpmModeler.CreateObject(CreateUselessId("eventObjectId"), endEvent.id, BPMConstants.EventWidth, BPMConstants.EventHeight, lastObject.Bounds.x + BPMConstants.HorizontalSpaceBetweenObjects, lastObject.Bounds.y);
            _bpmModeler.CreateFlow(CreateUselessId("flowId"), lastObject, endEventObject);
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
                    var gateway = _bpmModeler.CreateGateway(gatewayId, GatewayType.Parallel);
                    var sourceObjectsBounds = sourceObjects.Select(o => o.Bounds);
                    var gatewayY = sourceObjectsBounds.Select(b => b.y).Min() + ((sourceObjectsBounds.Select(b => b.y).Max() - sourceObjectsBounds.Select(b => b.y).Min()) / 2);
                    var gatewayX = sourceObjectsBounds.Select(b => b.x).Max() + BPMConstants.HorizontalSpaceBetweenObjects;
                    var gatewayObject = _bpmModeler.CreateObject(CreateUselessId("gatewayObjectId"), gatewayId, BPMConstants.GatewayWidth, BPMConstants.GatewayHeight, gatewayX, gatewayY);
                    foreach (var bpmObject in sourceObjects)
                    {
                        _bpmModeler.CreateFlow(CreateUselessId("flowId"), bpmObject, gatewayObject);
                    }
                    var destinationObject = _bpmModeler.CreateObject(CreateUselessId("taskObjectId"), destinationToSourcesId.Key, BPMConstants.TaskWidth, BPMConstants.TaskHeight, gatewayX + BPMConstants.HorizontalSpaceBetweenObjects, gatewayY);
                    _bpmModeler.CreateFlow(CreateUselessId("flowId"), gatewayObject, destinationObject);
                    _createdObjects.Add(destinationObject);

                }
                else
                {
                    var sourceObject = sourceObjects.FirstOrDefault();
                    var destinationObject = _bpmModeler.CreateObject(CreateUselessId("taskObjectId"), destinationToSourcesId.Key, BPMConstants.TaskWidth, BPMConstants.TaskHeight, sourceObject.Bounds.x + BPMConstants.HorizontalSpaceBetweenObjects, sourceObject.Bounds.y);
                    _bpmModeler.CreateFlow(CreateUselessId("flowId"), sourceObject, destinationObject);
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
                var gatewayY = startTasks.Count() / 2 * BPMConstants.VerticalSpaceBetweenObjects + BPMConstants.GatewayHeight;
                var startEventObject = _bpmModeler.CreateObject(CreateUselessId("eventObjectId"), startEvent.id, BPMConstants.EventWidth, BPMConstants.EventHeight, BPMConstants.EventWidth, gatewayY);
                _createdObjects.Add(startEventObject);
                x += BPMConstants.HorizontalSpaceBetweenObjects;
                var gatewayObject = _bpmModeler.CreateObject(CreateUselessId("gatewayObjectId"), gatewayId, BPMConstants.GatewayWidth, BPMConstants.GatewayHeight, x, gatewayY);
                _createdObjects.Add(gatewayObject);
                _bpmModeler.CreateFlow(CreateUselessId("flowId"), startEventObject, gatewayObject);
                x += BPMConstants.HorizontalSpaceBetweenObjects;
                foreach (var task in startTasks)
                {
                    var taskObject = _bpmModeler.CreateObject(CreateUselessId("taskObjectId"), task.id, BPMConstants.TaskWidth, BPMConstants.TaskHeight, x, y + BPMConstants.TaskHeight);
                    _createdObjects.Add(taskObject);
                    y += BPMConstants.VerticalSpaceBetweenObjects;
                    _bpmModeler.CreateFlow(CreateUselessId("flowId"), gatewayObject, taskObject);
                }

            }
            else
            {
                var startEventObject = _bpmModeler.CreateObject(CreateUselessId("eventObjectId"), startEvent.id, BPMConstants.EventWidth, BPMConstants.EventHeight, BPMConstants.EventWidth, BPMConstants.EventHeight);
                _createdObjects.Add(startEventObject);
                var taskObject = _bpmModeler.CreateObject(CreateUselessId("taskObjectId"), startTasks.SingleOrDefault().id, BPMConstants.TaskWidth, BPMConstants.TaskHeight, BPMConstants.HorizontalSpaceBetweenObjects, BPMConstants.TaskHeight);
                _createdObjects.Add(taskObject);
                _bpmModeler.CreateFlow(CreateUselessId("flowId"), startEventObject, taskObject);
            }
        }

        private void CreateEvents()
        {
            _createdEvents.Add(_bpmModeler.CreateEvent(CreateUselessId("startEventId"), EventType.Start));
            _createdEvents.Add(_bpmModeler.CreateEvent(CreateUselessId("endEventId"), EventType.End));
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
                    var destinationAttribute = connection.DestinationProperty.References.SingleOrDefault().Attribute;
                    var words = Regex.Matches(destinationAttribute.Name, @"(^[a-z]+|[A-Z]+(?![a-z])|[A-Z][a-z]+)")
                                            .Cast<Match>()
                                            .Select(m => m.Value.FirstCharToUpper());
                    taskName = string.Join(" ", words);

                    var valuesType = CreateValuesType(connection.HML.Types.FirstOrDefault(t => t.Id == destinationAttribute.Type));

                    var task = _bpmModeler.CreateTask(connection.Destination, taskName, taskType, valuesType);
                    _createdTasks.Add(task);

                }
                if (!_createdTasks.Any(t => t.id == connection.Source))
                {
                    taskType = destinationsProperties.Any(p => p == connection.SourceProperty) ? TaskType.BusinessRule : TaskType.User;
                    var sourceAttribute = connection.SourceProperty.References.SingleOrDefault().Attribute;
                    var words = Regex.Matches(sourceAttribute.Name, @"(^[a-z]+|[A-Z]+(?![a-z])|[A-Z][a-z]+)")
                                              .Cast<Match>()
                                              .Select(m => m.Value.FirstCharToUpper());
                    taskName = string.Join(" ", words);
                    var valuesType = CreateValuesType(connection.HML.Types.FirstOrDefault(t => t.Id == sourceAttribute.Type));

                    var task = _bpmModeler.CreateTask(connection.Source, taskName, taskType, valuesType);
                    _createdTasks.Add(task);
                }
                AddToDestinationSourcesDictionary(connection.Destination, connection.Source);
            }
        }

        private ValuesType CreateValuesType(Model.Type type)
        {
            var valuesType = new ValuesType
            {
                Constraints = Enumerable.Empty<DAR.API.Model.Constraint>()
            };
            switch (type.Base.FirstCharToUpper())
            {
                case (nameof(ValuesTypeName.Numeric)):
                    if (string.Equals(type.Name, nameof(ValuesTypeName.Boolean), StringComparison.InvariantCultureIgnoreCase))
                        valuesType.Name = ValuesTypeName.Boolean;
                    else
                    {
                        valuesType.Name = ValuesTypeName.Numeric;
                        valuesType.Constraints = CreateConstraints(type.Domain);
                    }
                    break;
                default:
                    valuesType.Name = ValuesTypeName.String;
                    break;
            }
            return valuesType;

        }

        private IEnumerable<DAR.API.Model.Constraint> CreateConstraints(Domain domain)
        {
            var constraints = new Collection<DAR.API.Model.Constraint>();

            foreach (var value in domain.Values)
            {

                if (!string.IsNullOrEmpty(value.From))
                    constraints.Add(new DAR.API.Model.Constraint
                    {
                        Config = value.From,
                        Name = ConstraintName.Min
                    });
                if (!string.IsNullOrEmpty(value.To))
                    constraints.Add(new DAR.API.Model.Constraint
                    {
                        Config = value.To,
                        Name = ConstraintName.Max
                    });
            }

            return constraints;
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