using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class NetworkTreeUpdater
    {
        private readonly Network _network;
        private readonly bool _updateHierarchyEnabled;
        private Period _defaultDateForPeriodVariables;

        public NetworkTreeUpdater(Network network, bool updateHiearchyEnabled, Period defaultDateForPeriodVariables)
        {
            _network = network;
            _updateHierarchyEnabled = updateHiearchyEnabled;
            _defaultDateForPeriodVariables = defaultDateForPeriodVariables;
        }

        public void UpdateFromSourceToTarget(XDocument source, XDocument target)
        {
            //_network.HierarchyModel.ReferencedObjects.All
            var nodes = new List<IStateObject>();
            nodes.AddRange(ProcessNodesElements(source, null, target.Root.Elements()));
            nodes.AddRange(FindNodesToDelete(source.Root.Elements()));
            //Update(_network.MyNodes);
            Update(nodes);
        }

        public void Update(IEnumerable<IStateObject> valuesForPeriod)
        {
            try
            {
                using (var context = ModelsDescription.GetModel(_network.Solution.ModelName))
                {
                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Networks.Attach(_network);
                    foreach (var v in valuesForPeriod)//.Where(s => s.State != ObjectState.Unchanged))
                    {
                        if (v.State == ObjectState.Added)
                        {
                            context.Set(v.GetType()).Add(v);
                        }
                        else
                        {
                            context.Set(v.GetType()).Attach(v);
                        }
                    }
                    context.FixState();
                    context.Database.Log = x => System.Diagnostics.Debug.WriteLine(x);
                    context.SaveChanges();
                }
            }
            catch(InvalidOperationException ex)
            {
                if(ex.Message.Contains("The changes to the database were committed successfully, but an error occurred while updating the object context. The ObjectContext might be in an inconsistent state."))
                {
                    return;
                }
            }
            catch (DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors.SelectMany(s => s.ValidationErrors);
                var message = string.Join(", ", errors.Select(e => e.ErrorMessage));
                throw new InvalidOperationException(message);
            }
        }

        private static string GetXPath(XElement element, List<string> parents)
        {
            if (element.Name == NetworkTree.RootName)
            {
                return $"/{NetworkTree.RootName}";
            }
            else
            {
                var grandParents = parents.Take(parents.Count - 1).ToList();
                return $"{GetXPath(element.Parent, grandParents)}/{element.Name}[@UniqueName='{parents.Last()}']";
            }
        }

        private IList<IStateObject> ProcessNodesElements(XDocument source, NetworkTreeNode parent, IEnumerable<XElement> elements)
        {
             var result = new List<IStateObject>();
            foreach (var element in elements.Where(e => e.Attribute("UniqueName") != null))
            {
                NetworkTreeNode node;
                var parentPath = new List<string>();
                if (parent != null)
                {
                    parentPath.AddRange(parent.Path.Split(NetworkTreeNode.PathSeparator));
                }
                parentPath.Add(element.Attribute("UniqueName").Value);
                var path = GetXPath(element, parentPath);
                var sourceElement = source.XPathSelectElement(path);
                if (sourceElement == null && _updateHierarchyEnabled)
                {
                    // this is new node
                    node = CreateNode(element, parent);
                    result.Add(node);
                    //_network.MyNodes.Add(node);
                }
                else
                {
                    // this is already existed node
                    var id = int.Parse(sourceElement.Attribute("NodeId").Value);
                    node = _network.MyNodes.Single(n => n.Id == id);
                    result.AddRange(UpdateNode(node, element));
                    node.MyNetwork = _network;
                    if (_updateHierarchyEnabled)
                    {
                        node.MyParent = parent;
                    }

                    //_network.MyNodes.Add(node);
                    result.Add(node);
                    sourceElement.Attribute("NodeId").Value = "0"; // признак того что элемент обработан
                }
                result.AddRange(ProcessNodesElements(source, node, element.Elements()));
            }
            return result;
        }

        private NetworkTreeNode CreateNode(XElement element, NetworkTreeNode parent)
        {
            var validFrom = ParseDateTimeString(element.Attribute("Since")?.Value);
            var validTill = ParseDateTimeString(element.Attribute("Till")?.Value);

            var node = (NetworkTreeNode)Activator.CreateInstance(_network.HierarchyModel.NodeType);
            node.MyNetwork = _network;
            node.MyParent = parent;
            node.ValidFrom = validFrom;
            node.ValidTill = validTill;
            node.State = ObjectState.Added;
            node.SetLinkedObject(CreateObject(element));
            node.LinkedObject.State = ObjectState.Added;

            foreach (var v in element.Elements())
            {
                if (v.Attribute("UniqueName") == null)
                {
                    node.LinkedObject.SetVariableValue(v.Name.LocalName, v.Value);
                }
            }
            return node;
        }

        private DateTime? ParseDateTimeString(string value)
        {
            return string.IsNullOrEmpty(value) ? new DateTime?() : DateTime.Parse(value);
        }

        private IEnumerable<IStateObject> UpdateNode(NetworkTreeNode node, XElement element)
        {
            var result = new List<IStateObject>();

            var validFrom = ParseDateTimeString(element.Attribute("Since")?.Value);
            var validTill = ParseDateTimeString(element.Attribute("Till")?.Value);

            if (node.ValidFrom != validFrom || node.ValidTill != validTill)
            {
                node.ValidFrom = validFrom;
                node.ValidTill = validTill;
                node.State = ObjectState.Modified;
            }

            var linkedObjectUpdated = false; // check that at least one field has been updated
            var targetObject = node.LinkedObject;

            foreach (var v in element.Elements().Where(v => v.Attribute("UniqueName") == null))
            {
                var varName = v.Name.LocalName;
                var parsedVar = VariableNameParser.GetVariableModel(targetObject.ObjectModel, varName);
                var model = parsedVar.VariableModel;
                if (!model.IsVersioned & model.PeriodInterval == TimeInterval.None)
                {
                    if (targetObject.SetVariableValue(varName, v.Value) && !linkedObjectUpdated)
                    {
                        linkedObjectUpdated = true;
                    }
                }

                if (model.IsVersioned & model.PeriodInterval == TimeInterval.Indefinite)
                {
                    // апериодические переменные здесь не обновляются, потому что для каждого такой переменной следует указывать различное время
                    var storedValue = targetObject.GetVariableValue(model, _defaultDateForPeriodVariables, parsedVar.Shift);
                    var currentValue = model.Parse(v.Value);
                    if (!Object.Equals(storedValue, currentValue))
                    {
                        var newVariable = Activator.CreateInstance(model.EntityType) as PeriodVariable;
                        newVariable.Date = _defaultDateForPeriodVariables.From.AddMonths(parsedVar.Shift);
                        newVariable.ObjectId = targetObject.Id;
                        newVariable.Value = currentValue;
                        newVariable.State = ObjectState.Added;
                        result.Add(newVariable);
                    }
                }

                if (model.IsVersioned & model.IsFixedPeriod)
                {
                    var storedValue = targetObject.GetVariableValue(model, _defaultDateForPeriodVariables, parsedVar.Shift);
                    var currentValue = model.Parse(v.Value);
                    if (!Object.Equals(storedValue, currentValue))
                    {
                        var newVariable = Activator.CreateInstance(model.EntityType) as PeriodVariable;
                        newVariable.ObjectId = targetObject.Id;
                        newVariable.Value = currentValue;
                        newVariable.Date = _defaultDateForPeriodVariables.From.AddMonths(parsedVar.Shift);
                        newVariable.State = ObjectState.Added;
                        result.Add(newVariable);
                    }
                }

                if(model.IsVersioned & model.PeriodInterval == TimeInterval.None)
                {
                    var storedValue = targetObject.GetVariableValue(model, _defaultDateForPeriodVariables, parsedVar.Shift);
                    var currentValue = model.Parse(v.Value);
                    if (!Object.Equals(storedValue, currentValue))
                    {
                        var newVariable = Activator.CreateInstance(model.EntityType) as VersionedVariable;
                        newVariable.ObjectId = targetObject.Id;
                        newVariable.Value = currentValue;
                        newVariable.State = ObjectState.Added;
                        result.Add(newVariable);
                    }
                }
            }

            if (linkedObjectUpdated)
            {
                targetObject.State = ObjectState.Modified;
            }

            return result;
        }

        private NamedObject CreateObject(XElement element)
        {
            var objName = element.Attribute("UniqueName").Value.Trim();
            var objectModel = ModelsDescription
                .All
                .Single(m => m.Name == _network.Solution.ModelName)
                .ObjectModels
                .Single(om => om.Name == element.Name.LocalName);

            var objectInstance = (NamedObject)Activator.CreateInstance(objectModel.ObjectType);
            //objectInstance.Solution = _network.Solution;
            objectInstance.SolutionId = _network.SolutionId;
            objectInstance.Name = objName;
            objectInstance.State = ObjectState.Added;

            return objectInstance;
        }

        private IList<NetworkTreeNode> FindNodesToDelete(IEnumerable<XElement> elements)
        {
            var result = new List<NetworkTreeNode>();
            foreach (var element in elements.Where(e => e.Attribute("NodeId") != null))
            {
                var attr = element.Attribute("NodeId");
                var id = int.Parse(attr.Value);
                if (id != 0)
                {
                    var node = _network.MyNodes.Single(n => n.Id == id);
                    node.State = ObjectState.Deleted;
                    result.Add(node);
                }
                result.AddRange(FindNodesToDelete(element.Elements()));
            }
            return result;
        }

    }
}
