using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingApi.Models.DTOs
{
    public class SubscriberCallsDTO
    {
        public long Id { get; set; }
        public long SubmissionId { get; set; }
        public string CallerId { get; set; }
        public string Recipient { get; set; }
        public DateTime CallDate { get; set; }
        public DateTime CallEnd { get; set; }
        public int Duration { get; set; }
        public Decimal Cost { get; set; }
        public string Reference { get; set; }
        public string Currency { get; set; }
    }
}
