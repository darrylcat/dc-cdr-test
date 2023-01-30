using FileUploadAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var result = await db.Submissions.AddAsync(submission);
                await db.SaveChangesAsync();
            }
            return submission;
        }
    }
}
