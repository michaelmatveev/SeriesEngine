using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesEngine.ExcelAddIn.Models;
using System.Linq;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Xml.Linq;
using System.Xml.XPath;
using Period = SeriesEngine.Core.DataAccess.Period;
using SeriesEngine.msk1;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.Tests.Database
{
    [TestClass]
    public class StoreNodesTests
    {
        public TestContext TestContext { get; set; }
        Solution _currentSolution;

        [TestInitialize]
        public void Init()
        {
            using (var context = new Model1())
            {
                var solution = new Solution
                {
                    Name = TestContext.Properties["SolutionName"] as string,
                    Description = "Test description"
                };

                var region = new Region()
                {
                    Solution = solution,
                    Name = "Region 1"
                };

                var consumer = new Consumer()
                {
                    Solution = solution,
                    Name = "Client 1"
                };

                var contract = new Contract()
                {
                    Solution = solution,
                    Name = "Contract 1",
                    ContractType = "Contract Type 1"
                };

                var network = new MainHierarchyNetwork()
                {
                    Name = "Main network 1",
                    Solution = solution
                };

                var node1 = new MainHierarchyNode()
                {
                    Region = region,
                    Parent = null,
                    Network = network
                };

                var node2 = new MainHierarchyNode()
                {
                    Consumer = consumer,
                    Parent = node1,
                    Network = network
                };

                var node3 = new MainHierarchyNode()
                {
                    Contract = contract,
                    Parent = node2,
                    Network = network
                };

                network.Nodes.Add(node1);
                network.Nodes.Add(node2);
                network.Nodes.Add(node3);
                context.Networks.Add(network);

                context.SaveChanges();

                _currentSolution = solution;
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            using (var context = new Model1())
            {
                var solution = context.Solutions.First(s => s.Id == _currentSolution.Id);
                context.Solutions.Remove(solution);

                context.SaveChanges();
            }
        }

        private static CollectionDataBlock GetCollectionDataBlock()
        {
            var collectionDataBlock = new CollectionDataBlock();
            collectionDataBlock.DataBlocks.Add(new NodeDataBlock(collectionDataBlock)
            {
                Parent = collectionDataBlock,
                NodeType = NodeType.UniqueName,
                RefObject = "Region",
                Level = 1
            });
            collectionDataBlock.DataBlocks.Add(new NodeDataBlock(collectionDataBlock)
            {
                Parent = collectionDataBlock,
                NodeType = NodeType.UniqueName,
                RefObject = "Consumer",
                Level = 2
            });
            collectionDataBlock.DataBlocks.Add(new NodeDataBlock(collectionDataBlock)
            {
                Parent = collectionDataBlock,
                NodeType = NodeType.UniqueName,
                RefObject = "Contract",
                Level = 3
            });
            return collectionDataBlock;
        } 

        private static void AssertElementExists(XDocument data, string path)
        {
            var element = data.XPathSelectElement(path);
            Assert.IsNotNull(element);
        }

        private static void AssertElementDoesNotExist(XDocument data, string path)
        {
            var element = data.XPathSelectElement(path);
            Assert.IsNull(element);
        }

        [TestMethod]
        [TestProperty("SolutionName", "Solution 1")]
        public void GetAllNodes()
        {
            var provider = new DataBaseNetworkProvider();
            var network = provider.GetNetwork(_currentSolution.Id, "Main network 1");
 
            var collectionDataBlock = GetCollectionDataBlock();            
            var dataXml = network.ConvertToXml(collectionDataBlock.DataBlocks, Period.Default);

            Console.WriteLine(dataXml);
            AssertElementExists(dataXml, "DataImportExport/Region[@UniqueName='Region 1']");
        }

        [TestMethod]
        [TestProperty("SolutionName", "Solution 2")]
        public void AddNodesAndSave()
        {
            var provider = new DataBaseNetworkProvider();
            var network = provider.GetNetwork(_currentSolution.Id, "Main network 1");

            var source = new XDocument(
                new XElement("DataImportExport"));
            var doc = new XDocument(
                new XElement("DataImportExport", 
                    new XElement("Region", new XAttribute("UniqueName", "Region 2"))));


            //network.LoadFromXml(source, doc);
            var collectionDataBlock = GetCollectionDataBlock();
            var dataXml = network.ConvertToXml(collectionDataBlock.DataBlocks, Period.Default);

            Console.WriteLine(dataXml);
            AssertElementExists(dataXml, "DataImportExport/Region[@UniqueName='Region 2']");
        }

        [TestMethod]
        [TestProperty("SolutionName", "Solution 3")]
        public void DeleteUpperLevelNodeAndSave()
        {
            var provider = new DataBaseNetworkProvider();
            var network = provider.GetNetwork(_currentSolution.Id, "Main network 1");

            var collectionDataBlock = GetCollectionDataBlock();
            var dataXml = network.ConvertToXml(collectionDataBlock.DataBlocks, Period.Default);
            var pathToDelete = "DataImportExport/Region[@UniqueName='Region 1']";
            var nodeId = int.Parse(dataXml.XPathSelectElement(pathToDelete).Attribute("NodeId").Value);
            network.DeleteObjectLinkedWithNode(nodeId);

            network = provider.GetNetwork(_currentSolution.Id, "Main network 1");

            dataXml = network.ConvertToXml(collectionDataBlock.DataBlocks, Period.Default);
            Console.WriteLine(dataXml);

            AssertElementDoesNotExist(dataXml, pathToDelete);
        }

        [TestMethod]
        [TestProperty("SolutionName", "Solution 4")]
        public void DeleteLowLevelNodeAndSave()
        {
            var provider = new DataBaseNetworkProvider();
            var network = provider.GetNetwork(_currentSolution.Id, "Main network 1");

            var collectionDataBlock = GetCollectionDataBlock();
            var dataXml = network.ConvertToXml(collectionDataBlock.DataBlocks, Period.Default);
            var xPathToDelete = "DataImportExport/Region/Consumer/Contract[@UniqueName='Contract 1']";
            var nodeId = int.Parse(dataXml.XPathSelectElement(xPathToDelete).Attribute("NodeId").Value);
            network.DeleteObjectLinkedWithNode(nodeId);

            network = provider.GetNetwork(_currentSolution.Id, "Main network 1");

            dataXml = network.ConvertToXml(collectionDataBlock.DataBlocks, Period.Default);
            Console.WriteLine(dataXml);

            AssertElementDoesNotExist(dataXml, xPathToDelete);
            AssertElementExists(dataXml, "DataImportExport/Region/Consumer[@UniqueName='Client 1']");
        }

    }
}