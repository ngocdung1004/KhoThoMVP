using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhoThoMVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerScheduleController : ControllerBase
    {
        private readonly IWorkerScheduleService _scheduleService;

        public WorkerScheduleController(IWorkerScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerScheduleDto>>> GetAll()
        {
            var schedules = await _scheduleService.GetAllSchedulesAsync();
            return Ok(schedules);
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerScheduleDto>> GetById(int id)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null) return NotFound();
            return Ok(schedule);
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpGet("worker/{workerId}")]
        public async Task<ActionResult<IEnumerable<WorkerScheduleDto>>> GetByWorkerId(int workerId)
        {
            var schedules = await _scheduleService.GetSchedulesByWorkerIdAsync(workerId);
            return Ok(schedules);
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpPost]
        public async Task<ActionResult<WorkerScheduleDto>> Create(CreateWorkerScheduleDto dto)
        {
            var schedule = await _scheduleService.CreateScheduleAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = schedule.ScheduleID }, schedule);
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpPut("{id}")]
        public async Task<ActionResult<WorkerScheduleDto>> Update(int id, CreateWorkerScheduleDto dto)
        {
            var schedule = await _scheduleService.UpdateScheduleAsync(id, dto);
            if (schedule == null) return NotFound();
            return Ok(schedule);
        }
        [Authorize(Roles = "0")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _scheduleService.DeleteScheduleAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
