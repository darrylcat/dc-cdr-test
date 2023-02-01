using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportingApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingApi.Controllers.Tests
{
    [TestClass()]
    public class SubscriberControllerTests
    {

        private static SubscriberController testObj { get; set; }


        [TestInitialize()]
        public void Setup()
        {
            if(testObj == null)
            {
                testObj = new SubscriberController();
            }
        }

        [TestMethod()]
        public void ListTest()
        {
            var actual = ;
        }
    }
}