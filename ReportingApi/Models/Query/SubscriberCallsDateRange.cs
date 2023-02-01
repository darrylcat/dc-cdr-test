using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingApi.Models.Query
{
    public class SubscriberCallsDateRange
    {
        public string Subscriber { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
