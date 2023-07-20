using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using External.ThirdParty.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TranslationManagement.Data.Configuration;
using TranslationManagement.Services.Contracts;
using TranslationManagement.Services.DTO;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class TranslationJobController : BaseController
    {
        private readonly ITranslationJobService _translationJobService;
        private readonly ILogger<TranslationJobController> _logger;
        public TranslationJobController(ILogger<TranslationJobController> logger, ITranslationJobService translationJobService)
        {
            _logger = logger;
            _translationJobService = translationJobService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TranslatorJobDto>>> GetJobs()
        {
            return Ok(await _translationJobService.GetJobs());
        }

        [HttpPost]
        public async Task<ActionResult> CreateJob([FromBody] CreateTranslatorJobRequestDto job)
        {
            
            int createdId = await _translationJobService.AddJob(job);

            if (createdId > 0)
            {
                var notificationSvc = new UnreliableNotificationService();

                await _retryPolicy.ExecuteAsync(async () =>
                {
                    if (await notificationSvc.SendNotification("Job created: " + createdId))
                    {
                        _logger.LogInformation("New job notification sent");
                    }
                });
            }

            return Ok();
        }

        [HttpPost]
        [Route("CreateJobWithFile")]
        public async Task<ActionResult> CreateJobWithFile(IFormFile file, string customer)
        {
            return await CreateJob(_translationJobService.ProcessJobFile(file, customer));
        }


        [HttpPatch("{jobId}/newStatus")]
        public async Task<ActionResult> UpdateJobStatus(int jobId, int newStatus)
        {
            _logger.LogInformation("Job status update request received, statusid: " + newStatus + " for job " + jobId.ToString());

            await _translationJobService.UpdateTranslatorJobStatus(jobId, newStatus);
            return Ok(Constants.translatorJobUpdatedStatusMessage);
        }

        //implement endpoints that will allow to track (set and get) which translator works on what job
        [HttpPatch]
        [Route("AssignTranslatorJob")]
        public async Task<ActionResult> AssignTranslatorJob(int translatorId, int jobId)
        {
            await _translationJobService.AssignTranslatorJob(translatorId, jobId);
            return Ok();
        }

        [HttpPost]
        [Route("GetTranslatorJobs")]
        public async Task<ActionResult> GetTranslatorJobsById(int translatorId)
        {
            var jobs = await _translationJobService.GetTranslatorJobsById(translatorId);
            if (!jobs.Any())
            {
                return NoContent();
            }
            return Ok(jobs);
        }
    }
}