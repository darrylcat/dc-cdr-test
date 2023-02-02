using Microsoft.EntityFrameworkCore;
using ReportingApi.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingApiTests3.TestUtils
{
    public class ReportingContextInMemoryDbFactory : IDbContextFactory<ReportingContext>
    {
        private readonly DbContextOptions<ReportingContext> options;

        public ReportingContextInMemoryDbFactory(DbContextOptions<ReportingContext> options)
        {
            this.options = options;
        }

        public ReportingContext CreateDbContext()
        {
            return new ReportingContext(options);
        }
    }
}
