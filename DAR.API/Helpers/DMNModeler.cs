using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using DAR.API.Constants;
using DAR.API.Extensions;
using DAR.API.Model.DMN;

namespace DAR.API.Helpers
{
    public class DMNModeler
    {

        private (int, int) _lastDecisionCoords;
        private readonly tDefinitions _root;
        public DMNModeler()
        {
            _root = new tDefinitions
            {
                id = "Decision_" + Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", ""),
                name = "DRD",
                @namespace = "http://camunda.org/schema/1.0/dmn"
            };
            _lastDecisionCoords = (0, BPMConstants.VerticalSpaceBetweenObjects * 2);
        }

        public string AddDecision(string name)
        {
            var id = name.ToCamelCase() + "Id";
            var currentDecisions = _root.Items;
            var resultDecisions = new tDecision[(currentDecisions?.Length ?? 0) + 1];
            if (currentDecisions != null)
                Array.Copy(currentDecisions, resultDecisions, currentDecisions.Length);
            var newDecision = new tDecision
            {
                name = name + " Decision",
                id = id,
                extensionElements = new tDMNElementExtensionElements(),
            };
            AddBounds(newDecision);
            newDecision.Item = new tDecisionTable
            {
                id = name.ToCamelCase() + "TableId"
            };
            resultDecisions[resultDecisions.Length - 1] = newDecision;


            _root.Items = resultDecisions;

            return id;

        }

        private void AddBounds(tDecision decision)
        {
            decision.extensionElements.Bounds =
                new Bounds()
                {
                    x = _lastDecisionCoords.Item1,
                    y = _lastDecisionCoords.Item2,
                    width = BPMConstants.DecisionWidth,
                    height = BPMConstants.DecisionHeight
                };
            _lastDecisionCoords.Item1 += BPMConstants.HorizontalSpaceBetweenObjects / 2;
        }
        public void AddInput(string name, string decisionId, string type, string referenceId)
        {
            var decision = _root.Items.FirstOrDefault(d => d.id == decisionId) as tDecision;
            var decisionTable = decision.Item as tDecisionTable;
            var currentInputs = decisionTable.input;
            var resultInputs = new tInputClause[(currentInputs?.Length ?? 0) + 1];
            if (currentInputs != null)
                Array.Copy(currentInputs, resultInputs, currentInputs.Length);

            var input = new tInputClause
            {
                id = name.ToCamelCase() + "InputId",
                label = name + " Input",
                inputExpression = new tLiteralExpression
                {
                    id = name.ToCamelCase() + "InputExpressionId",
                    typeRef = type,
                    Item = referenceId
                }
            };
            resultInputs[resultInputs.Length - 1] = input;
            decisionTable.input = resultInputs;
        }

        public string AddOutput(string name, string decisionId, string type)
        {
            var id = name.ToCamelCase() + "OutputId";
            var decision = _root.Items.FirstOrDefault(d => d.id == decisionId) as tDecision;
            var decisionTable = decision.Item as tDecisionTable;
            var currentOutputs = decisionTable.output;
            var resultOutputs = new tOutputClause[(currentOutputs?.Length ?? 0) + 1];
            if (currentOutputs != null)
                Array.Copy(currentOutputs, resultOutputs, currentOutputs.Length);
            resultOutputs[resultOutputs.Length - 1] = new tOutputClause
            {
                label = name + " Output",
                name = id,
                id = id,
                typeRef = type
            };
            decisionTable.output = resultOutputs;
            return id;

        }

        public void CreateConnection(string sourceDecisionId, string destinationDecisionId)
        {
            var sourceDecision = _root.Items.FirstOrDefault(d => d.id == sourceDecisionId) as tDecision;
            var destinationDecision = _root.Items.FirstOrDefault(d => d.id == destinationDecisionId) as tDecision;
            var sourceBounds = sourceDecision.extensionElements.Bounds;
            var destinationBounds = destinationDecision.extensionElements.Bounds;

            var currentEdges = destinationDecision.extensionElements.Edge;
            var resultEdges = new DMNEdge[(currentEdges?.Length ?? 0) + 1];
            if (currentEdges != null)
                Array.Copy(currentEdges, resultEdges, currentEdges.Length);

            resultEdges[resultEdges.Length - 1] = new DMNEdge
            {
                Source = sourceDecisionId,
                waypoint = new Point[2]{
                    new Point{
                        x=sourceBounds.x,
                        y=sourceBounds.y + BPMConstants.DecisionHeight/2
                    },
                    new Point{
                    x=destinationBounds.x,
                        y=destinationBounds.y + BPMConstants.DecisionHeight/2
                    }
                }
            };
            destinationDecision.extensionElements.Edge = resultEdges;
            AddRequirement(sourceDecisionId, destinationDecisionId);

        }

        public void AddRequirement(string sourceDecisionId, string destinationDecisionId)
        {
            var destinationDecision = _root.Items.FirstOrDefault(d => d.id == destinationDecisionId) as tDecision;
            var currentRequirements = destinationDecision.informationRequirement;
            var resultRequirements = new tInformationRequirement[(currentRequirements?.Length ?? 0) + 1];
            if (currentRequirements != null)
                Array.Copy(currentRequirements, resultRequirements, currentRequirements.Length);
            resultRequirements[resultRequirements.Length - 1] = new tInformationRequirement
            {
                Item = new tDMNElementReference
                {
                    href = "#" + sourceDecisionId
                },
                ItemElementName = ItemChoiceType.requiredDecision
            };
            destinationDecision.informationRequirement = resultRequirements;
        }

        public void Save(string filename)
        {
            using (var file = File.Create(filename + ".dmn"))
            {
                var serializer = new XmlSerializer(typeof(tDefinitions));
                serializer.Serialize(file, _root);
            }

        }
    }
}