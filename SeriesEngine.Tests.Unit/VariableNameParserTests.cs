using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.msk1;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;

namespace SeriesEngine.Tests.Unit
{
    [TestClass]
    public class VariableNameParserTests
    {
        [TestMethod]
        public void GetElementNameWithNegativeShift()
        {
            var elementName = VariableNameParser.GetVariableElementName(new VariableDataBlock(null)
            {
                VariableMetamodel = msk1ElectricMeterVariables.Integral,
                Shift = -1                
            });
            Assert.AreEqual("Integral.-1", elementName);
        }
        
        [TestMethod]
        public void GetElementNameWithPossitieveShift()
        {
            var elementName = VariableNameParser.GetVariableElementName(new VariableDataBlock(null)
            {
                VariableMetamodel = msk1ElectricMeterVariables.Integral,
                Shift = 1
            });
            Assert.AreEqual("Integral.1", elementName);
        }

        [TestMethod]
        public void GetElementNameWithoutShift()
        {
            var elementName = VariableNameParser.GetVariableElementName(new VariableDataBlock(null)
            {
                VariableMetamodel = msk1ElectricMeterVariables.Integral,
            });
            Assert.AreEqual("Integral", elementName);
        }

        [TestMethod]
        public void GetVariableModelWithNegativeShift()
        {
            var pv = VariableNameParser.GetVariableModel(msk1Objects.ElectricMeter, "Integral.-1");
            Assert.AreEqual(-1, pv.Shift);
            Assert.AreEqual(msk1ElectricMeterVariables.Integral, pv.VariableModel);
        }

        [TestMethod]
        public void GetVariableModelWithPositiveShift()
        {
            var pv = VariableNameParser.GetVariableModel(msk1Objects.ElectricMeter, "Integral.1");
            Assert.AreEqual(1, pv.Shift);
            Assert.AreEqual(msk1ElectricMeterVariables.Integral, pv.VariableModel);
        }

        [TestMethod]
        public void GetVariableModelWithoutShift()
        {
            var pv = VariableNameParser.GetVariableModel(msk1Objects.ElectricMeter, "Integral");
            Assert.AreEqual(0, pv.Shift);
            Assert.AreEqual(msk1ElectricMeterVariables.Integral, pv.VariableModel);
        }

    }
}
