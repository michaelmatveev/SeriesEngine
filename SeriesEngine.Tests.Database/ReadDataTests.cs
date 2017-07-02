using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.Core;
using System.Linq;
using System.Data.Entity;
using SeriesEngine.abc;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SeriesEngine.Tests.Database
{
    [TestClass]
    public class ReadDataTests
    {
        public TestContext TestContext { get; set; }
        private string _neworkName;
        private Solution _currentSolution;

        [TestInitialize]
        public void Init()
        {
            using (var context = new abc.Model1())
            {
                _currentSolution = new Solution
                {
                    Name = TestContext.Properties["SolutionName"] as string,
                    ModelName = ModelsDescription.abc.Name,
                    Description = "Test description"
                };

                var objectA1 = new ObjectA
                {
                    Solution = _currentSolution,
                    Name = "A1"
                };

                var v1 = new ObjectA_VariableA01
                {
                    ObjectA = objectA1,
                    VariableA01 = "Value 1",
                    Date = new DateTime(2012, 01, 01)
                };
                objectA1.ObjectA_VariableA01s.Add(v1);

                var objectA2 = new ObjectA
                {
                    Solution = _currentSolution,
                    Name = "A2"
                };

                var objectA1B1 = new ObjectB
                {
                    Solution = _currentSolution,
                    Name = "B1"
                };

                _neworkName = "Main network 1";
                var network = new FirstHierarchyNetwork
                {
                    Name = _neworkName,
                    Solution = _currentSolution
                };

                var node1 = new FirstHierarchyNode
                {
                    ObjectA = objectA1,
                    Parent = null,
                    Network = network
                };
                network.Nodes.Add(node1);

                var node2 = new FirstHierarchyNode
                {
                    ObjectA = objectA2,
                    Parent = null,
                    Network = network
                };
                network.Nodes.Add(node2);

                var node3 = new FirstHierarchyNode
                {
                    ObjectB = objectA1B1,
                    Parent = node1,
                    Network = network
                };
                network.Nodes.Add(node3);

                context.Networks.Add(network);

                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                context.SaveChanges();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            using (var context = new abc.Model1())
            {
                context.Solutions.Attach(_currentSolution);
                context.Solutions.Remove(_currentSolution);
                context.SaveChanges();
            }
        }

        private static bool ElementExists(XDocument data, string path)
        {
            var element = data.XPathSelectElement(path);
            return element != null;
        }

        private static string ElementValue(XDocument data, string path)
        {
            var element = data.XPathSelectElement(path);
            return element?.Value;
        }

        [TestMethod]
        [TestProperty("SolutionName", "Database test 1")]
        public void GetVariables()
        {
            DataBlock[] blocks =
            {   
                new NodeDataBlock(null)
                {
                    NodeType = NodeType.UniqueName,
                    RefObject = abcObjects.ObjectA.Name,
                    Visible = true,
                    Level = 1
                },
                new VariableDataBlock(null)
                {
                    VariableMetamodel = abcObjectAVariables.VariableA01,
                    RefObject = abcObjects.ObjectA.Name,
                    Visible = true,
                    Level = 1,
                    VariablePeriod = new Period
                    {
                        From = new DateTime(2012, 01, 01),
                        Till = new DateTime(2012, 02, 01)
                    }
                }
            };

            var provider = new DataBaseNetworkProvider();
            var network = provider.GetNetwork(_currentSolution, _neworkName, blocks);

            var xml = network.ConvertToXml(blocks, Period.Default, "*");
            Console.WriteLine(xml);

            Assert.IsTrue(ElementExists(xml, "DataImportExport/ObjectA[@UniqueName='A1']"));
            Assert.AreEqual("Value 1", ElementValue(xml, "DataImportExport/ObjectA[@UniqueName='A1']/VariableA01"));
        }

        [TestMethod]
        [TestProperty("SolutionName", "Database test 2")]
        public void GetVariablesForTimeRange()
        {
            DataBlock[] blocks =
            {
                new NodeDataBlock(null)
                {
                    NodeType = NodeType.UniqueName,
                    RefObject = abcObjects.ObjectA.Name,
                    Visible = true,
                    Level = 1
                },
                new VariableDataBlock(null)
                {
                    VariableMetamodel = abcObjectAVariables.VariableA01,
                    RefObject = abcObjects.ObjectA.Name,
                    Visible = true,
                    Level = 1,
                    VariablePeriod = new Period
                    {
                        From = new DateTime(2017, 01, 01),
                        Till = new DateTime(2017, 02, 01)
                    }
                }
            };

            var provider = new DataBaseNetworkProvider();
            var period = new Period
            {
                From = new DateTime(2017, 01, 01),
                Till = new DateTime(2017, 02, 01)
            };

            var network = provider.GetNetwork(_currentSolution, _neworkName, blocks, period);

            var xml = network.ConvertToXml(blocks, Period.Default, "*");
            Console.WriteLine(xml);

            Assert.IsTrue(ElementExists(xml, "DataImportExport/ObjectA[@UniqueName='A1']"));
            Assert.AreEqual(string.Empty, ElementValue(xml,"DataImportExport/ObjectA[@UniqueName='A1']/VariableA01"));
        }

        [TestMethod]
        [TestProperty("SolutionName", "Database test 3")]
        public void GetNodesForPath()
        {
            DataBlock[] blocks =
            {
                new NodeDataBlock(null)
                {
                    NodeType = NodeType.UniqueName,
                    RefObject = abcObjects.ObjectA.Name,
                    Visible = true,
                    Level = 1
                },
                new NodeDataBlock(null)
                {
                    NodeType = NodeType.UniqueName,
                    RefObject = abcObjects.ObjectB.Name,
                    Visible = true,
                    Level = 2
                }
            };

            var provider = new DataBaseNetworkProvider();
            var period = new Period
            {
                From = new DateTime(2017, 01, 01),
                Till = new DateTime(2017, 02, 01)
            };

            var network = provider.GetNetwork(_currentSolution, _neworkName, blocks, period);

            var xml = network.ConvertToXml(blocks, Period.Default, "*");
            Console.WriteLine(xml);

            Assert.IsTrue(ElementExists(xml, "DataImportExport/ObjectA[@UniqueName='A1']"));
            Assert.IsTrue(ElementExists(xml, "DataImportExport/ObjectA[@UniqueName='A2']"));
            Assert.IsTrue(ElementExists(xml, "DataImportExport/ObjectA/ObjectB[@UniqueName='B1']"));

            xml = network.ConvertToXml(blocks, Period.Default, "/A1");
            Console.WriteLine(xml);

            Assert.IsTrue(ElementExists(xml, "DataImportExport/ObjectA[@UniqueName='A1']"));
            Assert.IsFalse(ElementExists(xml, "DataImportExport/ObjectA[@UniqueName='A2']"));
            Assert.IsTrue(ElementExists(xml, "DataImportExport/ObjectA/ObjectB[@UniqueName='B1']"));

            xml = network.ConvertToXml(blocks, Period.Default, "/A1/B1");
            Console.WriteLine(xml);

            Assert.IsTrue(ElementExists(xml, "DataImportExport/ObjectA[@UniqueName='A1']"));
            Assert.IsFalse(ElementExists(xml, "DataImportExport/ObjectA[@UniqueName='A2']"));
            Assert.IsTrue(ElementExists(xml, "DataImportExport/ObjectA/ObjectB[@UniqueName='B1']"));
        }

    }
}