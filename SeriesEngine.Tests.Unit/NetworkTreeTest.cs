using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Collections.Generic;

namespace SeriesEngine.Tests.Unit
{
    [TestClass]
    public class NetworkTreeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var network = new abc.FirstHierarchyNetwork();
            var networkTree = new NetworkTree(network, true);
            var queryParamters = new List<DataBlock>
            {

            };
            var doc = networkTree.ConvertToXml(queryParamters, Period.Default, string.Empty);
            Console.WriteLine(doc);
            Assert.AreEqual("<DataImportExport />", doc.ToString());
        }
    }
}
