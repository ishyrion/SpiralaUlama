using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpiralaUlamaLibrary;

namespace SpiralaUlamaUnitTest
{
    [TestClass]
    class SpiralaUlamaTests
    {

        [TestMethod]
        [Timeout(2000)]
        [DataRow(1,1000)]
        [DataRow(10,1)]
        public void IsTypeOfSpiralaUlamaGenerator(double value1, double value2)
        {
            Assert.IsInstanceOfType(value: new SpiralaUlamaGenerator(), expectedType: typeof(SpiralaUlamaGenerator));
            Tuple<List<Tuple<int, int>>, double> tuple = 
                new Tuple<List<Tuple<int, int>>, double>(new List<Tuple<int, int>>(), 0);
            Assert.IsInstanceOfType(new SpiralaUlamaGenerator().GetValues(value1, value2, out tuple), 
                typeof(SpiralaUlamaGenerator));
            Assert.IsInstanceOfType(new SpiralaUlamaGenerator().GenerujPunkty(), 
                typeof(SpiralaUlamaGenerator));

        }

        [TestMethod]
        [Timeout(2000)]
        [DataRow(-10, -01)]
        [DataRow(-100,-0.00123)]
        [DataRow(0,0)]
        [DataRow(10,10)]
        [DataRow(-1,100)]
        public void DoesWrongDataThrowExceptionSpiralaUlamaGenerator(double value1, double value2)
        {
            try
            {
                Tuple<List<Tuple<int, int>>, double> tuple =
                    new Tuple<List<Tuple<int, int>>, double>(new List<Tuple<int, int>>(), 0);
                Assert.IsInstanceOfType(new SpiralaUlamaGenerator().GetValues(value1, value2, out tuple),
                    typeof(SpiralaUlamaGenerator));
            }
            catch (Exception ex)
            {
                Assert.Fail("No exception was thrown.");
                Assert.IsNull(ex);
            }

        }


        //GetValues(double maxHeight, double maxWidth, out Tuple<List<Tuple<int, int>>, double> toReturn)

        [TestMethod]
        [Timeout(2000)]
        [DataRow(-123, 0)]
        [DataRow(-4, 0)]
        [DataRow(-3, 0)]
        [DataRow(-2, 0)]
        [DataRow(-1, 0)]
        [DataRow(0, 0)]
        [DataRow(1, 0)]
        [DataRow(2, 1)]
        [DataRow(3, 2)]
        [DataRow(4, 2)]
        [DataRow(5, 3)]
        [DataRow(6, 3)]
        [DataRow(7, 4)]
        [DataRow(8, 4)]
        [DataRow(9, 4)]
        [DataRow(10, 4)]
        [DataRow(11, 5)]
        [DataRow(12, 5)]
        [DataRow(13, 6)]
        [DataRow(30, 10)]
        [DataRow(100, 25)]
        [DataRow(200, 46)]
        [DataRow(500, 95)]
        [DataRow(1000, 168)]
        public void DoesSpiralaUlamaHaveRightAmountOfPoints(int generatePrimesToValue, int expectedNumberOfPrimesAndPoints)
        {
            Tuple<List<Tuple<int, int>>, double> tuple =
                 new Tuple<List<Tuple<int, int>>, double>(new List<Tuple<int, int>>(), 0);

            PrimeNumbersGenerator.GetSingleton().FindPrimesTo(generatePrimesToValue);
            SpiralaUlamaGenerator spirala = new SpiralaUlamaGenerator().GetValues(100,100, out tuple);

            Assert.Equals(tuple.Item1.Count, expectedNumberOfPrimesAndPoints);
            Assert.IsTrue(tuple.Item2 > 0);

           // Assert.IsInstanceOfType(new SpiralaUlamaGenerator().GetValues(value1, value2, out tuple),


        }

    }
}
