using FileUploadAPI.Model;
using Microsoft.EntityFrameworkCore;
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

        public virtual void Setup()
        {
            CreateDbContextFactory();
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

        public virtual void Cleanup()
        {

        }

        protected string GetFileContents(string fileName)
        {
            byte[] fileData = File.ReadAllBytes(fileName);
            return Convert.ToBase64String(fileData);
        }


    }
}
