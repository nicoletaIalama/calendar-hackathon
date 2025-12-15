using Calendar.Domain.Entities;
using Calendar.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly Holiday[] holidays =
        {
            new() { 
                Id = new Guid("3cf84a94-5972-4d31-bc43-34014bf829bd"),
                EmployeeId = new Guid("4e503abc-1000-4dd7-8f34-09bb1a023301"),
                Start = new DateTimeOffset(2025, 12, 22, 8, 30, 0, TimeSpan.FromHours(0)),
                End = new DateTimeOffset(2026, 1, 2, 17, 30, 0, TimeSpan.FromHours(0)),
                Type = HolidayType.AnnualLeave,
                CreatedAt = DateTime.UtcNow
            },
        };

        [HttpGet]
        public async Task<IActionResult> GetAllHolidays()
        {
            return Ok(holidays);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHolidayById(Guid id)
        {
            var holiday = holidays.FirstOrDefault(h => h.Id == id);
            if (holiday == null)
            {
                return NotFound();
            }
            return Ok(holiday);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetHolidaysByEmployeeId(Guid employeeId)
        {
            var employeeHolidays = holidays.Where(h => h.EmployeeId == employeeId).ToArray();
            return Ok(employeeHolidays);
        }

        [HttpPost()]
        public async Task<IActionResult> GetHolidayForEmployees([FromBody] Guid[] employeeIds)
        {
            var employeeHolidays = holidays.Where(holidays => employeeIds.Any(e => e == holidays.EmployeeId)).ToArray();
            return Ok(employeeHolidays);
        }
    }
}
