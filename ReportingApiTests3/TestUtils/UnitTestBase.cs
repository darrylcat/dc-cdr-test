using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using ReportingApi.Models.Database;
using ReportingApiTests3.TestUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingApi.TestUtils
{
    public class UnitTestBase
    {

        protected static string CSV_FILE = @"C:\Users\darryl.catchpol\source\repos\CDR-Test\FileUploadAPITests\TestData\techtest_cdr.csv";
        protected static IDbContextFactory<ReportingContext> dbContextFactory { get; set; }
        protected static IWebHostEnvironment WebHostEnvironment { get; set; }

        protected static string TEST_CALL_ID_1 = "123456789";

        protected static int FILE_LINES = 13035;

        public virtual void Setup()
        {
            CreateDbContextFactory();
            InitaliseWebHostEnvironment();
            Task.Run(async () => await InitialiseDatabase()).Wait();
        }

        private async Task InitialiseDatabase()
        {
            using(var db = dbContextFactory.CreateDbContext())
            {
                db.RemoveRange(db.CallRecords);
                db.RemoveRange(db.Submissions);
                await db.SaveChangesAsync();

                var submission = new Submission() { Added = DateTime.UtcNow, Filename = "foobar.csv", SavedFilename = @"\\dfs\path\to\imported\file.csv", UserId = 1 };
                db.Submissions.Add(submission);
                await db.SaveChangesAsync();
                db.CallRecords.Add(new CallRecord() { 
                    CallDate = DateTime.UtcNow.AddYears(-2),
                    CallEnd = DateTime.MinValue.AddMinutes(5),
                    CallerId = TEST_CALL_ID_1,
                    Cost = 0.011m,
                    Currency = "GBP",
                    Duration = 25,
                    Recipient = "423887878",
                    Reference = "GBR0804504",
                    SubmissionId = submission.Id
                });
                db.CallRecords.Add(new CallRecord()
                {
                    CallDate = DateTime.UtcNow.AddDays(-1),
                    CallEnd = DateTime.MinValue.AddMinutes(5),
                    CallerId = TEST_CALL_ID_1,
                    Cost = 0.000m,
                    Currency = "GBP",
                    Duration = 20,
                    Recipient = "423887878",
                    Reference = "GBR0804505",
                    SubmissionId = submission.Id
                });
                db.CallRecords.Add(new CallRecord()
                {
                    CallDate = DateTime.UtcNow.AddMinutes(-10),
                    CallEnd = DateTime.MinValue.AddMinutes(5),
                    CallerId = TEST_CALL_ID_1,
                    Cost = 0.020m,
                    Currency = "GBP",
                    Duration = 19,
                    Recipient = "423887878",
                    Reference = "GBR0804505",
                    SubmissionId = submission.Id
                });
                await db.SaveChangesAsync();
            }
        }

        private void CreateDbContextFactory()
        {
            if(dbContextFactory == null)
            {
                var ob = new DbContextOptionsBuilder<ReportingContext>();
                ob.UseInMemoryDatabase("InMemDb");
                dbContextFactory = new ReportingContextInMemoryDbFactory(ob.Options);
            }
        }

        private void InitaliseWebHostEnvironment()
        {
            if(WebHostEnvironment == null)
            {
                var mock = new Mock<IWebHostEnvironment>();
                mock.Setup(x => x.WebRootPath).Returns(@"C:\Users\darryl.catchpol\source\repos\TestUpload\FileUploadApi\wwwroot\");
                WebHostEnvironment = mock.Object;
            }
        }

        public virtual void Cleanup()
        {

        }

        protected string GetFileContents(string fileName)
        {
            byte[] fileData = File.ReadAllBytes(fileName);
            return Convert.ToBase64String(fileData);
        }

        protected IFormFile GetFileForm()
        {
            string fileName = @"C:\Users\darryl.catchpol\source\repos\CDR-Test\FileUploadAPITests\TestData\techtest_cdr.csv";
            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            return new FormFile(fs, 0, fs.Length, Path.GetFileName(fileName), fileName);
        }

    }
}
