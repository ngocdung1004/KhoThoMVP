using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KhoThoMVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkersController(IWorkerService workerService)
        {
            _workerService = workerService;
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerDto>>> GetWorkers()
        {
            var workers = await _workerService.GetAllWorkersAsync();
            return Ok(workers);
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerDto>> GetWorker(int id)
        {
            try
            {
                var worker = await _workerService.GetWorkerByIdAsync(id);
                return Ok(worker);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<WorkerDto>> CreateWorker(WorkerDto workerDto)
        //{
        //    var createdWorker = await _workerService.CreateWorkerAsync(workerDto);
        //    return CreatedAtAction(nameof(GetWorker), new { id = createdWorker.WorkerId }, createdWorker);
        //}
        [Authorize(Roles = "0, 1")]
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<WorkerDto>> CreateWorker([FromForm] CreateWorkerDto createWorkerDto)
        {
            try
            {
                var result = await _workerService.CreateWorkerAsync(createWorkerDto);
                return CreatedAtAction(nameof(GetWorker), new { id = result.WorkerId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpPost]
        //public async Task<ActionResult<WorkerDto>> CreateWorker(CreateWorkerDto createWorkerDto)
        //{
        //    var createdWorker = await _workerService.CreateWorkerAsync(createWorkerDto);
        //    return CreatedAtAction(nameof(GetWorker), new { id = createdWorker.WorkerId }, createdWorker);
        //}
        [Authorize(Roles = "0, 2")]
        [HttpPut("{id}")]
        public async Task<ActionResult<WorkerDto>> UpdateWorker(int id, WorkerDto workerDto)
        {
            try
            {
                var updatedWorker = await _workerService.UpdateWorkerAsync(id, workerDto);
                return Ok(updatedWorker);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize(Roles = "0")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            try
            {
                await _workerService.DeleteWorkerAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpGet("jobtype/{jobTypeId}")]
        public async Task<ActionResult<IEnumerable<WorkerDto>>> GetWorkersByJobType(int jobTypeId)
        {
            var workers = await _workerService.GetWorkersByJobTypeAsync(jobTypeId);
            return Ok(workers);
        }

        [Authorize(Roles = "0, 1, 2")]
        [HttpGet("me")]
        public async Task<ActionResult<WorkerDto>> GetCurrentWorker()
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                if (string.IsNullOrEmpty(userEmail))
                {
                    return BadRequest("User email not found in token");
                }

                var worker = await _workerService.GetWorkerByEmailAsync(userEmail);
                return Ok(worker);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
