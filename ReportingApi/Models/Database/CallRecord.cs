using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingApi.Models.Database
{
    public class CallRecord
    {
        [Key()]
        public long Id { get; set; }
        public long SubmissionId { get; set; }

        [MaxLength(20)]
        public string CallerId { get; set; }

        [MaxLength(20)]
        public string Recipient { get; set; }
        public DateTime CallDate { get; set; }
        public DateTime CallEnd { get; set; }
        public int Duration { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public Decimal Cost { get; set; }
        public string Reference { get; set; }
        [MaxLength(3)]
        public string Currency { get; set; }
    }
}
