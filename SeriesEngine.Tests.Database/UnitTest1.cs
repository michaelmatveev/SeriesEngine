using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesEngine.msk1;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.Tests.Database
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var context = new Model1())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                var network = context.Networks.Find(31);
                var netId = new SqlParameter("@NetId", 31);
                var path = new SqlParameter("@PathToFind", "Пензенская область|ООО \"МагнитЭнерго\"|1001014-ЭН|ММ \"Версаль\" г. Пенза  ул. Ладожская, 139|ТП-775");
                //var result = context.Database.SqlQuery<MainHierarchyNode>("[msk1].[ReadMainHierarchy] @NetId, @PathToFind", netId, path).ToListAsync().Result;
                var result = context.MainHierarchyNodes.SqlQuery("[msk1].[ReadMainHierarchy] @NetId, @PathToFind", netId, path).ToListAsync().Result;
                Console.WriteLine(result.Count);
                //var query = context.MainHierarchyNodes
                //    .Include("Region")
                //    .Include("Consumer")
                //    .Include("Contract")
                //    .Include("ConsumerObject")
                //    .Include("Point")
                //    .Include("ElectricMeter");
                var query = context.Entry(network).Collection("Nodes").Query()
    .Include("Region")
    .Include("Consumer")
    .Include("Contract")
    .Include("ConsumerObject")
    .Include("Point")
    .Include("ElectricMeter");
                query.Load();


                foreach (var q in network.MyNodes)
                {
                    Console.WriteLine(q.LinkedObject?.Name);
                }

                //foreach(var n in result)
                //{
                //    context.Entry(n).Reference(p => p.Region).Load();
                //    context.Entry(n).Reference(p => p.Point).Load();
                //}
                //foreach (var n in result)
                //{
                //    Console.WriteLine(n.LinkedObject?.Name);
                //}
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            using (var context = new Model1())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                var network = context.Networks.Find(31);
                var netId = new SqlParameter("@NetId", 31);
                var path = new SqlParameter("@PathToFind", "Пензенская область|ООО \"МагнитЭнерго\"|1001014-ЭН|ММ \"Версаль\" г. Пенза  ул. Ладожская, 139|ТП-775");
                //             var result = context.Database.SqlQuery<MainHierarchyNode>("[msk1].[ReadMainHierarchy] @NetId, @PathToFind", netId, path).ToListAsync().Result;
                var ids = context.Database.SqlQuery<int>("[msk1].[MainHierarchy_Read] @NetId, @PathToFind", netId, path).ToListAsync().Result;
                var d = new DateTime(2014, 01, 01);
                var nodes = context.MainHierarchyNodes
                    .Where(n => ids.Contains(n.Id))
                    .Include("Network")
                    .Include("Region");
                nodes = nodes
                    .Include(n => n.Consumer)
                    .Include(n => n.Contract)
                    .Include(n => n.ConsumerObject)
                    .Include(n => n.Point)
                    .Include(n => n.ElectricMeter)
                    .Include(n => n.Point.Point_ContractVoltageLevels);
                nodes.ToList();
                    //.Where(n => n.Point.Point_VoltageLevels.);

                //var result = context.MainHierarchyNodes.SqlQuery("[msk1].[ReadMainHierarchy] @NetId, @PathToFind", netId, path).ToListAsync().Result;
                Console.WriteLine(ids.Count);
                Console.WriteLine(nodes.Count());
                Console.WriteLine(network.MyNodes.Count);
                foreach(var n in nodes)
                {
                    Console.WriteLine($"{n.LinkedObject.Name}");
                }
                
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            using (var context = new Model1())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                var network = context.Networks.Find(31);
                var netId = new SqlParameter("@NetId", 31);
                var path = new SqlParameter("@PathToFind", "Пензенская область|ООО \"МагнитЭнерго\"|1001014-ЭН|ММ \"Версаль\" г. Пенза  ул. Ладожская, 139|ТП-775");
                //             var result = context.Database.SqlQuery<MainHierarchyNode>("[msk1].[ReadMainHierarchy] @NetId, @PathToFind", netId, path).ToListAsync().Result;
                var ids = context.Database.SqlQuery<int>("[msk1].[MainHierarchy_Read] @NetId, @PathToFind", netId, path).ToListAsync().Result;

                var nodes = context.Entry(network).Collection("Nodes").Query();

                //var nodes = context.MainHierarchyNodes
                //context.Set<MainHierarchyNode>() //(network.HierarchyModel.NodeType)
                //.Where(n => ids.Contains(n.Id)).AsQueryable();

                nodes
                    .Include("Region")
                    .Include("Consumer")
                    .Include("Contract")
                    .Include("ConsumerObject")
                    .Include("Point")
                    .Include("ElectricMeter");
                    
                nodes.Load();
                //.Include(n => n.Region)
                //.Include(n => n.Consumer)
                //.Include(n => n.Contract)
                //.Include(n => n.ConsumerObject)
                //.Include(n => n.Point)
                //.Include(n => n.ElectricMeter)
                //.Include(n => n.Point.Point_VoltageLevels);
                //.Where(n => n.Point.Point_VoltageLevels.);

                //var result = context.MainHierarchyNodes.SqlQuery("[msk1].[ReadMainHierarchy] @NetId, @PathToFind", netId, path).ToListAsync().Result;
                Console.WriteLine(ids.Count);
                //Console.WriteLine(nodes.Count());
                foreach (var n in nodes)
                {
                    var n1 = n as NetworkTreeNode;
                    if (ids.Contains(n1.Id))
                    {
                        Console.WriteLine($"{n1.LinkedObject.Name}");
                    }
                }

            }
        }
    }
}
