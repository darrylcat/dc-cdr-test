using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportingApi.Models.Database;
using ReportingApi.Models.DTOs;
using ReportingApi.Models.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingApi.Controllers
{
    //TO-DO: Add [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly IDbContextFactory<ReportingContext> contextFactory;

        public SubscriberController(IDbContextFactory<ReportingContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        // TO-DO: Policy for check user has priviledges to run List
        [HttpGet()]
        public async Task<ICollection<SubscriberCallsDTO>> List(SubscriberCallsDateRange subscriberCallsDateRange)
        {
            if (subscriberCallsDateRange.From == null) subscriberCallsDateRange.From = DateTime.MinValue;
            if (subscriberCallsDateRange.To == null) subscriberCallsDateRange.To = DateTime.UtcNow;
            var db = contextFactory.CreateDbContext();
            var count = db.CallRecords.Count();
            return await db.CallRecords.Where(
                x => x.CallerId == subscriberCallsDateRange.Subscriber &&
                x.CallDate >= (DateTime)subscriberCallsDateRange.From &&
                x.CallDate <= (DateTime)subscriberCallsDateRange.To).Select(x => new SubscriberCallsDTO()
                {
                    Id = x.Id,
                    CallerId = x.CallerId,
                    CallDate = x.CallDate,
                    CallEnd = x.CallEnd,
                    Cost = x.Cost,
                    Currency = x.Currency,
                    Duration = x.Duration,
                    Recipient = x.Recipient,
                    Reference = x.Reference,
                    SubmissionId = x.SubmissionId
                }).ToListAsync<SubscriberCallsDTO>(); ;
        }

    }
}
