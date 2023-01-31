using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileUploadAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadAPI.Extensions.Tests
{
    [TestClass()]
    public class DataParserTests
    {
        [TestMethod()]
        public void ShortDateTest()
        {
            DateTime expected = new DateTime(2022, 05, 15);
            DateTime actual = "15/05/2022".ShortDate();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ShortTimeTest()
        {
            DateTime expected = new DateTime(1, 1, 1, 14, 15, 18);
            DateTime actual = "14:15:18".ShortTime();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ThreeDecimalPalcesTest()
        {
            string expected = "25.482";
            decimal actual = expected.ThreeDecimalPlaces();
            Assert.AreEqual(expected, actual.ToString());
        }
    }
}