using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportingApi.Controllers;
using ReportingApi.Models.DTOs;
using ReportingApi.Models.Query;
using ReportingApi.TestUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingApi.Controllers.Tests
{
    [TestClass()]
    public class SubscriberControllerTests : UnitTestBase
    {

        private static SubscriberController testObj { get; set; }


        [TestInitialize()]
        public override void Setup()
        {
            base.Setup();
            if(testObj == null)
            {
                testObj = new SubscriberController(dbContextFactory);
            }
        }

        [TestMethod()]
        public async Task ListTest()
        {
            var query = new SubscriberCallsDateRange() { 
                From = DateTime.UtcNow.AddDays(-365),
                To = DateTime.UtcNow.AddDays(1),
                Subscriber = "0123456789"
            };
            var actual = await testObj.List(query);
            Assert.IsInstanceOfType(actual, typeof(ICollection<SubscriberCallsDTO>));
        }
    }
}