using Calendar.Domain.DashboardModels;
using Calendar.Domain.Entities;
using Calendar.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Api;

[Route("api/[controller]")]
[ApiController]
public class HolidayController : ControllerBase
{
    public HolidayController(DashboardDbContext dashboardDbContext)
    {
        _dashboardDbContext = dashboardDbContext ?? throw new ArgumentNullException(nameof(dashboardDbContext));
    }

    private readonly DashboardDbContext _dashboardDbContext;

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
    public async Task<IActionResult> GetHolidaysByDate(DateTime date, CancellationToken ct)
    {
        var holidays = await _dashboardDbContext.Holidays
            .Where(h => h.HolidayDate == date)
            .ToListAsync(ct);

        if (holidays == null)
        {
            return NotFound();
        }
        return Ok(holidays);
    }

    [HttpGet("employee/{initials}")]
    public async Task<IActionResult> GetHolidaysByEmployeeId(string initials)
    {
        var employeeHolidays = await _dashboardDbContext.Holidays
            .Where(o => o.Initials == initials)
            .ToArrayAsync();

        return Ok(employeeHolidays);
    }

    [HttpPost()]
    public async Task<IActionResult> GetHolidayForEmployees([FromBody] Guid[] employeeIds)
    {
        var employeeHolidays = holidays.Where(holidays => employeeIds.Any(e => e == holidays.EmployeeId)).ToArray();
        return Ok(employeeHolidays);
    }
}
