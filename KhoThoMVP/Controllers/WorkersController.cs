using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerDto>>> GetWorkers()
        {
            var workers = await _workerService.GetAllWorkersAsync();
            return Ok(workers);
        }

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

        [HttpPost]
        public async Task<ActionResult<WorkerDto>> CreateWorker(WorkerDto workerDto)
        {
            var createdWorker = await _workerService.CreateWorkerAsync(workerDto);
            return CreatedAtAction(nameof(GetWorker), new { id = createdWorker.WorkerId }, createdWorker);
        }

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

        [HttpGet("jobtype/{jobTypeId}")]
        public async Task<ActionResult<IEnumerable<WorkerDto>>> GetWorkersByJobType(int jobTypeId)
        {
            var workers = await _workerService.GetWorkersByJobTypeAsync(jobTypeId);
            return Ok(workers);
        }
    }
}
