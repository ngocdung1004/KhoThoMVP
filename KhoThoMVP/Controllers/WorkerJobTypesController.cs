using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhoThoMVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerJobTypesController : ControllerBase
    {
        private readonly IWorkerJobTypeService _workerJobTypeService;

        public WorkerJobTypesController(IWorkerJobTypeService workerJobTypeService)
        {
            _workerJobTypeService = workerJobTypeService;
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerJobTypeDto>>> GetAllWorkerJobTypes()
        {
            var workerJobTypes = await _workerJobTypeService.GetAllWorkerJobTypesAsync();
            return Ok(workerJobTypes);
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerJobTypeDto>> GetWorkerJobType(int id)
        {
            var workerJobType = await _workerJobTypeService.GetWorkerJobTypeByIdAsync(id);
            if (workerJobType == null) return NotFound();
            return Ok(workerJobType);
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpGet("worker/{workerId}")]
        public async Task<ActionResult<IEnumerable<WorkerJobTypeDto>>> GetWorkerJobTypesByWorkerId(int workerId)
        {
            var workerJobTypes = await _workerJobTypeService.GetWorkerJobTypesByWorkerIdAsync(workerId);
            return Ok(workerJobTypes);
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpPost]
        public async Task<ActionResult<WorkerJobTypeDto>> CreateWorkerJobType(WorkerJobTypeDto workerJobTypeDto)
        {
            var workerJobType = await _workerJobTypeService.CreateWorkerJobTypeAsync(workerJobTypeDto);
            return CreatedAtAction(nameof(GetWorkerJobType), new { id = workerJobType.WorkerJobTypeId }, workerJobType);
        }
        [Authorize(Roles = "0, 2")]
        [HttpPut("{id}")]
        public async Task<ActionResult<WorkerJobTypeDto>> UpdateWorkerJobType(int id, WorkerJobTypeDto workerJobTypeDto)
        {
            var workerJobType = await _workerJobTypeService.UpdateWorkerJobTypeAsync(id, workerJobTypeDto);
            if (workerJobType == null) return NotFound();
            return Ok(workerJobType);
        }
        [Authorize(Roles = "0, 2")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWorkerJobType(int id)
        {
            await _workerJobTypeService.DeleteWorkerJobTypeAsync(id);
            return NoContent();
        }
    }
}
