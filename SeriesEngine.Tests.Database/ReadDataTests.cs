using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.Core;
using System.Linq;
using System.Data.Entity;
using SeriesEngine.abc;

namespace SeriesEngine.Tests.Database
{
    [TestClass]
    public class ReadDataTests
    {
        public TestContext TestContext { get; set; }
        private Solution _currentSolution;

        [TestInitialize]
        public void Init()
        {
            using (var context = new abc.Model1())
            {
                _currentSolution = new Solution
                {
                    Name = TestContext.Properties["SolutionName"] as string,
                    ModelName = "abs",
                    Description = "Test description"
                };

                //var objectA = new ObjectA
                //{
                //    Solution = _currentSolution,
                //    Name = "A1"
                //};

                var network = new FirstHierarchyNetwork
                {
                    Name = "Main network 1",
                    Solution = _currentSolution
                };

                //var node1 = new FirstHierarchyNode
                //{
                //    ObjectA = objectA,
                //    Parent = null,
                //    Network = network
                //};

                //network.Nodes.Add(node1);
                context.Networks.Add(network);

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

        [TestMethod]
        [TestProperty("SolutionName", "Database test 1")]
        public void GetVariables()
        {

        }
    }
}
