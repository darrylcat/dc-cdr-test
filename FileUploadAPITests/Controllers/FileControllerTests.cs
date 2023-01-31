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
                testObj = new FileController(dbContextFactory);
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
            var expected = CreateSubmission();
            var actual = await testObj.PostAsync(expected);
            Assert.AreEqual(expected.Filename, actual.Filename);
            Assert.AreEqual(expected.Data, actual.Data);
            Assert.AreEqual(expected.UserId, actual.UserId);
        }

        [TestMethod()]
        public async Task IdFieldShouldBeSet()
        {
            var expected = CreateSubmission();
            var actual = await testObj.PostAsync(expected);
            Assert.IsTrue(actual.Id > 0);
        }

        [TestMethod()]
        public async Task DataIsAddedToCrdRecord()
        {
            var submission = CreateSubmission();
            var result = await testObj.PostAsync(submission);
            using (var db = dbContextFactory.CreateDbContext())
            {
                var actual = await db.CallRecords.ToListAsync();
                Assert.IsInstanceOfType(actual, typeof(List<CallRecord>));
            }
           
        }

        [TestMethod()]
        public async Task ImportDataHasCorrectNoOfRecords()
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                db.CallRecords.RemoveRange(db.CallRecords);
                await db.SaveChangesAsync();
                var submission = CreateSubmission();
                var result = await testObj.PostAsync(submission);
                var actual = db.CallRecords.Count();
                Assert.AreEqual(FILE_LINES, actual);
            }
        }

        private Submission CreateSubmission()
        {
            return new Submission() { Filename = "techtest_cdr.csv", UserId = 1, Data = GetFileContents(CSV_FILE) };
        }


    }
}