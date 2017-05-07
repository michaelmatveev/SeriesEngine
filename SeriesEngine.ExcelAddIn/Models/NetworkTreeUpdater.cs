﻿using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.msk1;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class NetworkTreeUpdater
    {
        private readonly Network _network;
        private DateTime _defaultDateForPeriodVariables;
        public NetworkTreeUpdater(Network network, DateTime defaultDateForPeriodVariables)
        {
            _network = network;
            _defaultDateForPeriodVariables = defaultDateForPeriodVariables;
        }

        public void LoadFromXml(XDocument source, XDocument target)
        {
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
                using (var context = new Model1())
                {
                    //context.Configuration.AutoDetectChangesEnabled = false;
                    //context.Networks.Attach(_network);
                    foreach (var v in valuesForPeriod)
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
            catch (DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors.SelectMany(s => s.ValidationErrors);
                var message = string.Join(", ", errors.Select(e => e.ErrorMessage));
                throw new InvalidOperationException(message);
            }
        }

        private static string GetXPath(XElement element)
        {
            if (element == null)
            {
                return string.Empty;
            }
            else
            {
                return $"{GetXPath(element.Parent)}/{element.Name}";
            }
        }

        private IList<IStateObject> ProcessNodesElements(XDocument source, NetworkTreeNode parent, IEnumerable<XElement> elements)
        {
            var result = new List<IStateObject>();
            foreach (var element in elements.Where(e => e.Attribute("UniqueName") != null))
            {
                NetworkTreeNode node;
                var path = $"{GetXPath(element)}[@UniqueName='{element.Attribute("UniqueName").Value}']";
                var sourceElement = source.XPathSelectElement(path);
                if (sourceElement == null)
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
                    result.AddRange(UpdateNode(node, element, parent));
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

        private IEnumerable<IStateObject> UpdateNode(NetworkTreeNode node, XElement element, NetworkTreeNode parent)
        {
            var result = new List<IStateObject>();

            var validFrom = ParseDateTimeString(element.Attribute("Since")?.Value);
            var validTill = ParseDateTimeString(element.Attribute("Till")?.Value);

            node.MyNetwork = _network;
            node.MyParent = parent;
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
                var model = targetObject.ObjectModel.Variables.Single(m => m.Name == varName);
                if (!model.IsVersioned & !model.IsPeriodic)
                {
                    if (targetObject.SetVariableValue(varName, v.Value) && !linkedObjectUpdated)
                    {
                        linkedObjectUpdated = true;
                    }
                }

                if(model.IsVersioned & model.IsPeriodic)
                {
                    var newVariable = Activator.CreateInstance(model.EntityType) as PeriodVariable;
                    newVariable.ObjectId = targetObject.Id;
                    newVariable.Value = model.Parse(v.Value);
                    newVariable.Date = _defaultDateForPeriodVariables;
                    newVariable.State = ObjectState.Added;
                    result.Add(newVariable);
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