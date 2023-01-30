using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadAPI.Model
{
    public class FileUploadContext : DbContext 
    {
        public DbSet<Submission> Submissions { get; set; }

        public FileUploadContext(DbContextOptions<FileUploadContext> options) : base(options)
        {
        }

    }
}
