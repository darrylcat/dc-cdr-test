using FileUploadAPI.Extensions;
using FileUploadAPI.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // TO-DO: Add [Authorize]
    public class FileController : ControllerBase
    {
        private readonly IDbContextFactory<FileUploadContext> contextFactory;
        private readonly IWebHostEnvironment hostingEnvironment;

        public FileController(IDbContextFactory<FileUploadContext> contextFactory, IWebHostEnvironment hostingEnvironment)
        {
            this.contextFactory = contextFactory;
            this.hostingEnvironment = hostingEnvironment;
        }

        // TO-DO: Add Role Claim Policy
        [HttpPost()]
        public async Task<Submission> PostAsync(IFormFile formFile)
        {
            
            var userId = 1; // assuming userId is one for now - would nomally get from this.User.Claims;
            string wwwPath = hostingEnvironment.WebRootPath;

            string path = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Path.Combine(path, Guid.NewGuid().ToString() + ".csv");
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            var submission = new Submission() { Added = DateTime.UtcNow, Filename = formFile.FileName, SavedFilename = fileName, UserId = userId };
            using (var db = contextFactory.CreateDbContext())
            {
                int count = 0;
                var result = await db.Submissions.AddAsync(submission);
                await db.SaveChangesAsync();
                
                IEnumerable<string> csvRecords = System.IO.File.ReadLines(fileName, Encoding.UTF8);
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
                                SubmissionId = submission.Id,
                                CallerId = fields[0],
                                Recipient = fields[1],
                                CallDate = fields[2].ShortDate(),
                                CallEnd = fields[3].ShortTime(),
                                Duration = int.Parse(fields[4]),
                                Cost = fields[5].ThreeDecimalPlaces(),
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
