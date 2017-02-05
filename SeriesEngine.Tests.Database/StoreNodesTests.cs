using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesEngine.Msk1;
using SeriesEngine.ExcelAddIn.Models;
using System.Linq;
using System.Collections.Generic;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Xml.Linq;
using System.Xml.XPath;

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

                var network = new Network()
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
                //var solution = context.Entry(_currentSolution);
                //solution.State = System.Data.Entity.EntityState.Deleted;
                //var solution = context.Solutions.Attach(_currentSolution);
                //context.Solutions.Remove(solution);

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

        private static void AssertData(XDocument data, string path)
        {
            var element = data.XPathSelectElement(path);
            Assert.IsNotNull(element);
        } 

        [TestMethod]
        [TestProperty("SolutionName", "Solution 1")]

        public void GetAllNodes()
        {
            var provider = new DataBaseNetworkProvider();
            var network = provider.GetNetworks(_currentSolution.Id)
                .Where(n => n.SolutionName == _currentSolution.Name)
                .First(n => n.Name == "Main network 1");

            var collectionDataBlock = GetCollectionDataBlock();
            var dataXml = XDocument.Parse(collectionDataBlock.GetXml(network));

            Console.WriteLine(dataXml);
            AssertData(dataXml, "DataImportExport/Region[@UniqueName='Region 1']");
        }

        [TestMethod]
        [TestProperty("SolutionName", "Solution 2")]

        public void AddNodesAndSave()
        {
            var provider = new DataBaseNetworkProvider();
            var network = provider.GetNetworks(_currentSolution.Id)
                .Where(n => n.SolutionName == _currentSolution.Name)
                .First(n => n.Name == "Main network 1");
 
            var doc = new XDocument(
                new XElement("DataImportExport", 
                    new XElement("Region", new XAttribute("UniqueName", "Region 2"))));
            network.LoadFromXml(doc);
            var collectionDataBlock = GetCollectionDataBlock();
            var dataXml = XDocument.Parse(collectionDataBlock.GetXml(network));

            Console.WriteLine(dataXml);
            AssertData(dataXml, "DataImportExport/Region[@UniqueName='Region 2']");
        }
    }
}
