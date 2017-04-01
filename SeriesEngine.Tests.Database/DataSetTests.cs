using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Linq;

namespace SeriesEngine.Tests.Database
{
    [TestClass]
    public class DataSetTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            DataSet dataSet1 = new DataSet("dataSet1");
            DataTable table = new DataTable("Items");
            dataSet1.Tables.Add(table);
            // Add columns
            //DataColumn c1 = new DataColumn("id", typeof(int), "");
            DataColumn c2 = new DataColumn("Item", typeof(string), "");
            //table.Columns.Add(c1);
            table.Columns.Add(c2);
            table.PrimaryKey = new[] { c2 };

            // Add ten rows.
            for (int i = 0; i < 10; i++)
            {
                DataRow row = table.NewRow();
                //row["id"] = i;
                row["Item"] = $"Item{i}";
                table.Rows.Add(row);
            }

            dataSet1.AcceptChanges();

            DataSet dataSet2 = new DataSet("dataSet2");
            var table2 = table.Clone();
            dataSet2.Tables.Add(table2);

            // Add ten rows.
            for (int i = 0; i < 5; i++)
            {
                DataRow row = table2.NewRow();
                //row["id"] = i;
                row["Item"] = $"Item{i}";
                table2.Rows.Add(row);
            }

            table2.Rows.Add(new[] { "Item12"});

            dataSet1.Merge(dataSet2, preserveChanges: true);
            PrintValues(dataSet1, "Merged With table.");
        }

        private void PrintValues(DataSet dataSet, string label)
        {
            Console.WriteLine("\n" + label);
            foreach (DataTable table in dataSet.Tables)
            {
                Console.WriteLine("TableName: " + table.TableName);
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        Console.Write($"{column.Caption} - {row[column]} \t");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
