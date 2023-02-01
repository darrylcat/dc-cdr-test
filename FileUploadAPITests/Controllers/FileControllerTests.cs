using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileUploadAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileUploadAPITests.TestUtils;
using FileUploadAPI.Model;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace FileUploadAPI.Controllers.Tests
{
    [TestClass()]
    public class FileControllerTests : UnitTestBase
    {

        private static FileController testObj { get; set; }

        [TestInitialize()]
        public override void Setup()
        {
            base.Setup();
            if(testObj == null)
            {
                testObj = new FileController(dbContextFactory, WebHostEnvironment);
            }
        }

        [TestCleanup()]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod()]
        public async Task PostAsyncTest()
        {
            var formFile = GetFileForm();
            var actual = await testObj.PostAsync(formFile);
            Assert.AreEqual(formFile.FileName, actual.Filename);
        }

        [TestMethod()]
        public async Task IdFieldShouldBeSet()
        {
            var formFile = GetFileForm();
            var actual = await testObj.PostAsync(formFile);
            Assert.IsTrue(actual.Id > 0);
        }

        [TestMethod()]
        public async Task DataIsAddedToCrdRecord()
        {
            var formFile = GetFileForm();
            var result = await testObj.PostAsync(formFile);
            using (var db = dbContextFactory.CreateDbContext())
            {
                var actual = await db.CallRecords.ToListAsync();
                Assert.IsInstanceOfType(actual, typeof(List<CallRecord>));
            }
           
        }

        [TestMethod()]
        public async Task ImportDataHasCorrectNoOfRecords()
        {
            var formFile = GetFileForm();
            using (var db = dbContextFactory.CreateDbContext())
            {
                db.CallRecords.RemoveRange(db.CallRecords);
                await db.SaveChangesAsync();
                var result = await testObj.PostAsync(formFile);
                var actual = db.CallRecords.Count();
                Assert.AreEqual(FILE_LINES, actual);
            }
        }

    }
}