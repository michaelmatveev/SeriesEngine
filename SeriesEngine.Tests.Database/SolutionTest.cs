using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.Tests.Database
{
    [TestClass]
    public class SolutionTest
    {
        [TestMethod]
        public void CreateSolution()
        {
            using (var context = new SeContext())
            {
                context.Solutions.Add(new Solution
                {
                    Name = "solution1",
                    Description = "test description"
                });
                context.SaveChanges();
            }
        }
    }
}
