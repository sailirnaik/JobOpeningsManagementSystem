using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.JobManagementAPI.Data.Interfaces;
using JobOpeningsManagementMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace JobOpeningsManagementMS.Controllers
{

    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        public JobController(IJobService jobService)
        {
            _jobService = jobService;

        }


        /// <summary>
        /// Creates a new job.
        /// </summary>
        /// <param name="dto">The job details.</param>
        /// <returns>The created job ID.</returns>
        [Authorize]
        [HttpPost]
        [Route("api/v1/jobs")]
        public async Task<IActionResult> Create([FromBody] CreateJobRequestDTO dto)
        {
            int id = _jobService.CreateJob(dto);
            var location = $"{Request.Scheme}://{Request.Host}/api/v1/jobs/{id}";

            return StatusCode(201, location);
        }

        /// <summary>
        /// Updates a new job.
        /// </summary>
        /// <param name="dto">The job details.</param>
        /// <returns>Success or not found message.</returns>
        [Authorize]
        [HttpPut]
        [Route("api/v1/jobs/{id}")]
        public IActionResult Update(int id, [FromBody] CreateJobRequestDTO dto)
        {
            try
            {
                bool success = _jobService.UpdateJob(id, dto);
                if (success)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Gets the list of jobs.
        /// </summary>
        /// <param name="request">The job details.</param>
        /// <returns>List of jobs</returns>
        [HttpPost]
        [Route("api/jobs/list")]
        public IActionResult GetListOfJobs([FromBody] ListOfJobsRequestDTO request)
        {
            var result = _jobService.GetListOfJobs(request);
            return Ok(result);
        }

        /// <summary>
        /// Gets the details of jobs based on ID.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Details of job</returns>
        [HttpGet]
        [Route("api/v1/jobs/{id}")]
        public IActionResult GetJobDetailsById(int id)
        {
            var result = _jobService.GetJobDetailsById(id);

            if (result == null)
                return NotFound("Job not found");

            return Ok(result);
        }
    }
}
