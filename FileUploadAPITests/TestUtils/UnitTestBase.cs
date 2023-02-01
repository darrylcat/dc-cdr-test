using FileUploadAPI.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadAPITests.TestUtils
{
    public class UnitTestBase
    {

        protected static string CSV_FILE = @"C:\Users\darryl.catchpol\source\repos\CDR-Test\FileUploadAPITests\TestData\techtest_cdr.csv";
        protected static IDbContextFactory<FileUploadContext> dbContextFactory { get; set; }
        protected static IWebHostEnvironment WebHostEnvironment { get; set; }

        protected static int FILE_LINES = 13035;

        public virtual void Setup()
        {
            CreateDbContextFactory();
            InitaliseWebHostEnvironment();
        }

        private void CreateDbContextFactory()
        {
            if(dbContextFactory == null)
            {
                var ob = new DbContextOptionsBuilder<FileUploadContext>();
                ob.UseInMemoryDatabase("InMemDb");
                dbContextFactory = new FileContextInMemoryDbFactory(ob.Options);
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
