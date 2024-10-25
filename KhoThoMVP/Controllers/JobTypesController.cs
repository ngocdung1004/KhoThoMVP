using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KhoThoMVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTypesController : ControllerBase
    {
        private readonly IJobTypeService _jobTypeService;

        public JobTypesController(IJobTypeService jobTypeService)
        {
            _jobTypeService = jobTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobTypeDto>>> GetAllJobTypes()
        {
            var jobTypes = await _jobTypeService.GetAllJobTypesAsync();
            return Ok(jobTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobTypeDto>> GetJobType(int id)
        {
            var jobType = await _jobTypeService.GetJobTypeByIdAsync(id);
            if (jobType == null) return NotFound();
            return Ok(jobType);
        }

        [HttpPost]
        public async Task<ActionResult<JobTypeDto>> CreateJobType(JobTypeDto jobTypeDto)
        {
            var jobType = await _jobTypeService.CreateJobTypeAsync(jobTypeDto);
            return CreatedAtAction(nameof(GetJobType), new { id = jobType.JobTypeId }, jobType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<JobTypeDto>> UpdateJobType(int id, JobTypeDto jobTypeDto)
        {
            var jobType = await _jobTypeService.UpdateJobTypeAsync(id, jobTypeDto);
            if (jobType == null) return NotFound();
            return Ok(jobType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJobType(int id)
        {
            await _jobTypeService.DeleteJobTypeAsync(id);
            return NoContent();
        }
    }
}
