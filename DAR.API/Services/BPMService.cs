using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BPMN;
using DAR.API.Model;

namespace DAR.API.Services
{
    public class BPMService : IBPMService
    {
        private readonly Editor _editor;
        private IDictionary<string, string> _propertyToActivity;
        private IDictionary<string, ICollection<string>> _destinationActivityToSourceActivities;
        private IDictionary<string, ICollection<string>> _startEventToDestinationActivites;
        private IDictionary<string, ICollection<string>> _endEventToSourceActivites;
        public BPMService(Editor editor)
        {
            _editor = editor ?? throw new System.ArgumentNullException(nameof(editor));
            _propertyToActivity = new Dictionary<string, string>();
            _destinationActivityToSourceActivities = new Dictionary<string, ICollection<string>>();
            _startEventToDestinationActivites = new Dictionary<string, ICollection<string>>();
            _endEventToSourceActivites = new Dictionary<string, ICollection<string>>();
        }

        public void CreateBPM(string filename, IEnumerable<Dependency> ard)
        {
            _editor.Create();

            CreateActivities(ard);
            ConnectActivities();
            _editor.Save(filename + ".bpmn");
        }

        private void ConnectActivities()
        {
            // ConnectStartAndEndEvents();

        }

        private void ConnectStartAndEndEvents()
        {
            string gatewayId;
            var startEventId = _startEventToDestinationActivites.First().Key;
            var endEventId = _endEventToSourceActivites.First().Key;
            var startEventDestinations = _startEventToDestinationActivites.First().Value;
            var endEventSources = _endEventToSourceActivites.First().Value;
            if (startEventDestinations.Count > 1)
            {
                gatewayId = _editor.AddGateway(null, null, GatewayType.Parallel, 1, startEventDestinations.Count);
                ConnectSourceAndDestination(startEventId, gatewayId);
                foreach (var activity in _startEventToDestinationActivites.First().Value)
                    ConnectSourceAndDestination(gatewayId, activity);
            }
            else
                ConnectSourceAndDestination(startEventId, startEventDestinations.First());

            // if (endEventSources.Count > 1)
            // {
            //     gatewayId = _editor.AddGateway(null, null, GatewayType.Parallel, endEventSources.Count, 1);
            //     ConnectSourceAndDestination(gatewayId, endEventId);
            //     foreach (var activity in endEventSources)
            //         ConnectSourceAndDestination(activity, gatewayId);
            // }
            // else
            //     ConnectSourceAndDestination(endEventSources.First(), endEventId);
        }

        private void ConnectSourceAndDestination(string source, string destination)
        {
            _editor.AddFlow(null, null, source, destination, null, FlowType.None, null, false, FlowDirection.None);
        }

        private void CreateActivities(IEnumerable<Dependency> ard)
        {
            var endEventId = _editor.AddEvent(null, null, "Start Event", EventType.Start, EventTrigger.None, EventRole.None);
            var startEventId = _editor.AddEvent(null, null, "End Event", EventType.End, EventTrigger.None, EventRole.None);
            var destinationsProperties = ard.Select(a => a.DestinationProperty);
            var sourceProperties = ard.Select(a => a.SourceProperty);
            foreach (var connection in ard)
            {
                string activityName;
                TaskType taskType = TaskType.BusinessRule;
                string destinationActivityId;
                string sourceActivityId;
                ICollection<string> activities;
                if (!_propertyToActivity.TryGetValue(connection.Destination, out destinationActivityId))
                {
                    activityName = connection.DestinationProperty.References.SingleOrDefault().Attribute.Name;
                    destinationActivityId = _editor.AddActivity(null, activityName, ActivityType.Task, ActivityMarker.None, taskType, null);
                    if (!sourceProperties.Any(p => p == connection.DestinationProperty))
                    {
                        if (_endEventToSourceActivites.TryGetValue(startEventId, out activities))
                            activities.Add(startEventId);
                        else
                        {
                            activities = new Collection<string>();
                            activities.Add(destinationActivityId);
                            _endEventToSourceActivites.Add(startEventId, activities);
                        }
                    }
                    _propertyToActivity.Add(connection.Destination, destinationActivityId);
                }
                if (!_propertyToActivity.TryGetValue(connection.Source, out sourceActivityId))
                {
                    activityName = connection.SourceProperty.References.SingleOrDefault().Attribute.Name;
                    if (!destinationsProperties.Any(p => p == connection.SourceProperty))
                    {
                        taskType = TaskType.User;
                        sourceActivityId = _editor.AddActivity(null, activityName, ActivityType.Task, ActivityMarker.None, taskType, null);
                        if (_startEventToDestinationActivites.TryGetValue(startEventId, out activities))
                            activities.Add(sourceActivityId);
                        else
                        {
                            activities = new Collection<string>();
                            activities.Add(sourceActivityId);
                            _startEventToDestinationActivites.Add(startEventId, activities);
                        }
                    }
                    else
                        sourceActivityId = _editor.AddActivity(null, activityName, ActivityType.Task, ActivityMarker.None, taskType, null);

                    _propertyToActivity.Add(connection.Source, sourceActivityId);
                }

                if (_destinationActivityToSourceActivities.TryGetValue(destinationActivityId, out activities))
                    activities.Add(sourceActivityId);
                else
                {
                    activities = new Collection<string>();
                    activities.Add(sourceActivityId);
                    _destinationActivityToSourceActivities.Add(destinationActivityId, activities);
                }
            }

        }
    }
}