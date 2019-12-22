using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DAR.API.Constants;
using DAR.API.Extensions;
using DAR.API.Model.BPM;
using DAR.API.Model;

namespace DAR.API.Helpers
{
    public class BPMModeler
    {
        private tDefinitions _root;
        private string processId;

        public ICollection<BPMNShape> CreatedObjects { get; set; }
        public string ProcessId
        {
            get => processId; set
            {
                processId = value;
                if (_root.process != null && _root.BPMNDiagram != null)
                {
                    _root.process[0].id = processId;
                    _root.BPMNDiagram[0].BPMNPlane.bpmnElement = new System.Xml.XmlQualifiedName(processId);
                }


            }
        }

        public BPMModeler()
        {
            _root = new tDefinitions();
            _root.targetNamespace = "http://bpmn.io/schema/bpmn";
            ProcessId = "DeployedProcess";
            _root.process = new tProcess[] {
                new tProcess{
                    id = ProcessId,
                    isExecutable = true,
                    isExecutableSpecified = true,
                    isClosed = false,
                    processType = tProcessType.None

        }};
            _root.BPMNDiagram = new BPMNDiagram[]{
            new BPMNDiagram{
                id = "diagram_1"
            }
        };
            _root.BPMNDiagram[0].BPMNPlane = new BPMNPlane()
            {
                bpmnElement = new System.Xml.XmlQualifiedName(ProcessId)
            };

        }


        public tTask CreateTask(string id, string name, TaskType type, ValuesType valuesType)
        {
            tTask[] currentTasks;
            tTask[] resultTasks;
            tTask task;
            switch (type)
            {
                case TaskType.User:

                    currentTasks = _root.process[0].userTask;
                    resultTasks = new tUserTask[(currentTasks?.Length ?? 0) + 1];
                    task = new tUserTask
                    {
                        name = "Insert " + name,
                        id = id
                    };

                    _root.process[0].userTask = resultTasks as tUserTask[];
                    var constraints = new Collection<Model.BPM.Constraint>();
                    foreach (var constraint in valuesType.Constraints)
                    {
                        constraints.Add(new Model.BPM.Constraint
                        {
                            Name = constraint.Name.GetDisplayName(),
                            Config = constraint.Config
                        });
                    }
                    task.extensionElements = new tExtensionElements
                    {
                        FormData = new FormData
                        {
                            FormField = new FormField
                            {
                                Label = name,
                                Id = id + "field",
                                Type = valuesType.Name.GetDisplayName(),
                                Validation = new Validation
                                {
                                    Constraints = constraints.ToArray()
                                }
                            }
                        }
                    };

                    break;
                default:
                    currentTasks = _root.process[0].businessRuleTask;
                    resultTasks = new tBusinessRuleTask[(currentTasks?.Length ?? 0) + 1];
                    _root.process[0].businessRuleTask = resultTasks as tBusinessRuleTask[];
                    task = new tBusinessRuleTask
                    {
                        name = "Calculate " + name,
                        id = id,
                        DecisionReference = "",
                        OutputType = valuesType.Name.GetDisplayName()
                    };

                    break;
            }

            if (currentTasks != null)
                Array.Copy(currentTasks, resultTasks, currentTasks.Length);
            resultTasks[resultTasks.Length - 1] = task;
            return task;
        }


        public tGateway CreateGateway(string id, GatewayType type)
        {
            tGateway gateway;
            switch (type)
            {

                default:
                    gateway = new tParallelGateway
                    {
                        id = id
                    };
                    var currentGateways = _root.process[0].parallelGateway;
                    var resultGateways = new tParallelGateway[(currentGateways?.Length ?? 0) + 1];
                    if (currentGateways != null)
                        Array.Copy(currentGateways, resultGateways, currentGateways.Length);
                    resultGateways[resultGateways.Length - 1] = gateway as tParallelGateway;
                    _root.process[0].parallelGateway = resultGateways;
                    break;
            }
            return gateway;
            // CreateShape(id, BPMConstants.GatewayWidth, BPMConstants.GatewayHeight);
        }
        public void CreateFlow(string id, BPMNShape source, BPMNShape destination)
        {
            var currentFlows = _root.process[0].sequenceFlow;
            var resultFlows = new tSequenceFlow[(currentFlows?.Length ?? 0) + 1];
            var flow = new tSequenceFlow
            {
                id = id,
                sourceRef = source.bpmnElement.ToString(),
                targetRef = destination.bpmnElement.ToString()
            };
            if (currentFlows != null)
                Array.Copy(currentFlows, resultFlows, currentFlows.Length);
            resultFlows[resultFlows.Length - 1] = flow;
            _root.process[0].sequenceFlow = resultFlows;
            var sourcePoint = new Point
            {
                x = source.Bounds.x + source.Bounds.width,
                y = source.Bounds.y + source.Bounds.height / 2
            };
            var destinationPoint = new Point
            {
                x = destination.Bounds.x,
                y = destination.Bounds.y + destination.Bounds.height / 2
            };
            CreateEdge(id, sourcePoint, destinationPoint);
        }



        public tEvent CreateEvent(string id, EventType type)
        {
            tEvent bpmEvent;
            switch (type)
            {
                case (EventType.Start):
                    bpmEvent = new tStartEvent
                    {
                        id = id
                    };
                    _root.process[0].startEvent = new tStartEvent[]
                    {
                        bpmEvent as tStartEvent
                    };
                    break;
                default:
                    bpmEvent = new tEndEvent
                    {
                        id = id
                    };
                    _root.process[0].endEvent = new tEndEvent[]
                    {
                        bpmEvent as tEndEvent
                    };
                    break;
            }
            return bpmEvent;

        }

        public BPMNShape CreateObject(string id, string referencingElement, double width, double height, double x, double y)
        {
            var currentShapes = _root.BPMNDiagram[0].BPMNPlane?.Shapes;
            var resultShapes = new BPMNShape[(currentShapes?.Length ?? 0) + 1];
            _root.BPMNDiagram[0].BPMNPlane.Shapes = resultShapes;
            if (currentShapes != null)
                Array.Copy(currentShapes, resultShapes, currentShapes.Length);
            var shape = new BPMNShape
            {
                bpmnElement = new System.Xml.XmlQualifiedName(referencingElement),
                id = id
            };
            AddBounds(shape, width, height, x, y);
            resultShapes[resultShapes.Length - 1] = shape;
            return shape;

        }

        private void AddBounds(BPMNShape shape, double width, double height, double x, double y)
        {
            shape.Bounds = new Bounds
            {
                width = width,
                height = height,
                x = x,
                y = y
            };
        }

        private void CreateEdge(string id, Point source, Point destination)
        {
            var currentEdges = _root.BPMNDiagram[0].BPMNPlane?.Edges;
            var resultEdges = new BPMNEdge[(currentEdges?.Length ?? 0) + 1];
            _root.BPMNDiagram[0].BPMNPlane.Edges = resultEdges;
            if (currentEdges != null)
                Array.Copy(currentEdges, resultEdges, currentEdges.Length);
            var edge = new BPMNEdge
            {
                bpmnElement = new System.Xml.XmlQualifiedName(id)
            };
            edge.waypoint = new Point[] {
                source,destination
            };

            resultEdges[resultEdges.Length - 1] = edge;

        }
        public void Save(string filename)
        {
            using (var file = File.Create(filename + ".bpmn"))
            {
                var serializer = new XmlSerializer(typeof(tDefinitions));
                serializer.Serialize(file, _root);
            }

        }
    }

    public enum TaskType
    {
        BusinessRule,
        User
    }
    public enum GatewayType
    {
        Parallel
    }
    public enum EventType
    {
        Start,
        End
    }


}