using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingApi.Models.Database
{
    public class Submission
    {
        [Key()]
        public long Id { get; set; }
        public string Filename { get; set; }
        public string SavedFilename { get; set; }
        public long UserId { get; set; }
        public DateTime Added { get; set; }
    }
}
