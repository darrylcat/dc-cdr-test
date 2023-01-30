using FileUploadAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadAPITests.TestUtils
{
    public class FileContextInMemoryDbFactory : IDbContextFactory<FileUploadContext>
    {
        private readonly DbContextOptions<FileUploadContext> options;

        public FileContextInMemoryDbFactory(DbContextOptions<FileUploadContext> options)
        {
            this.options = options;
        }

        public FileUploadContext CreateDbContext()
        {
            return new FileUploadContext(options);
        }
    }
}
