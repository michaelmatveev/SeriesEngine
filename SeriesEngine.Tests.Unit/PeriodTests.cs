using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.Tests.Unit
{
    [TestClass]
    public class PeriodTests
    {
        [TestMethod]
        public void Include()
        {
            var period = new Period
            {
                From = new DateTime(2017, 03, 01),
                Till = new DateTime(2017, 04, 01)
            };

            Assert.IsFalse(period.Include(new DateTime(2017, 02, 28, 23, 59, 59)));
            Assert.IsTrue(period.Include(new DateTime(2017, 03, 01)));
            Assert.IsTrue(period.Include(new DateTime(2017, 03, 02)));
            Assert.IsTrue(period.Include(new DateTime(2017, 03, 31, 23, 59, 59)));
            Assert.IsFalse(period.Include(new DateTime(2017, 04, 01)));
            Assert.IsFalse(period.Include(new DateTime(2017, 04, 02)));
        }

        [TestMethod]
        public void Default()
        {
            var now = DateTime.Now;
            var period = Period.Default;

            Assert.AreEqual(0, period.From.Minute);
            Assert.AreEqual(0, period.From.Hour);
            Assert.AreEqual(1, period.From.Day);
            Assert.AreEqual(now.Month, period.From.Month);

            Assert.AreEqual(0, period.Till.Minute);
            Assert.AreEqual(0, period.Till.Hour);
            Assert.AreEqual(1, period.Till.Day);
            Assert.AreEqual((now.Month + 1) % 12, period.Till.Month);

        }
    }
}