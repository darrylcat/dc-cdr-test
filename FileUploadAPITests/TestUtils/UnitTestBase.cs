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

        public virtual void Setup()
        {

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
