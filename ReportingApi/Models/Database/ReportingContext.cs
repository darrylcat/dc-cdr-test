using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingApi.Models.Database
{
    public class ReportingContext : DbContext
    {
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<CallRecord> CallRecords { get; set; }

        public ReportingContext(DbContextOptions<ReportingContext> options) : base(options)
        {
        }


    }
}
