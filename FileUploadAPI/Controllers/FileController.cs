using FileUploadAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IDbContextFactory<FileUploadContext> contextFactory;

        public FileController(IDbContextFactory<FileUploadContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        [HttpPost()]
        public async Task<Submission> PostAsync(Submission submission)
        {
            using (var db = contextFactory.CreateDbContext())
            {
                int count = 0;
                var result = await db.Submissions.AddAsync(submission);
                await db.SaveChangesAsync();
                byte[] data = Convert.FromBase64String(submission.Data);
                string[] csvRecords = Encoding.UTF8.GetString(data).Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                foreach(var record in csvRecords)
                {
                    count++;
                    if(count>1) // Ignore header row
                    {
                        var fields = record.Split(new string[] { "," }, StringSplitOptions.None);
                        if (fields.Length != 8)
                        {
                            // TO-DO: Log Error
                        }
                        else
                        {
                            var callRecord = new CallRecord()
                            {
                                CallerId = fields[0],
                                Recipient = fields[1],
                                CallDate = DateTime.Parse(fields[2]),
                                CallEnd = DateTime.Parse(fields[3]),
                                Duration = int.Parse(fields[4]),
                                Cost = decimal.Parse(fields[5]),
                                Reference = fields[6],
                                Currency = fields[7]
                            };
                            await db.CallRecords.AddAsync(callRecord);
                        }
                    }
                }
                await db.SaveChangesAsync();
            }
            return submission;
        }
    }
}
