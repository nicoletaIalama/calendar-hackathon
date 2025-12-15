using Calendar.Api.Domain.DashboardModels;
using Calendar.Api.Domain.Entities;
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

    public sealed record EmployeeHolidayRangeRequest(int[] EmployeeIds, DateTime StartDate, int Days);
    public sealed record EmployeeHolidayDayDto(int EmployeeId, DateTime HolidayDate, int HolidayType, decimal HolidaySize);

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

    [HttpGet("on/{date}")]
    public async Task<IActionResult> GetHolidaysByDate(DateTime date, CancellationToken cancellationToken)
    {
        var dashboardHolidays = await _dashboardDbContext.Holidays
            .Where(h => h.HolidayDate == date)
            .ToListAsync(cancellationToken);

        if (dashboardHolidays == null)
        {
            return NotFound();
        }

        return Ok(EmployeeHoliday(dashboardHolidays));
    }

    [HttpGet("employee/{employeeInitials}/{year}")]
    public async Task<IActionResult> GetHolidaysByEmployeeId(string employeeInitials, int year, CancellationToken cancellationToken)
    {
        var holidays = await _dashboardDbContext.Holidays
            .Where(h => h.Initials == employeeInitials)
            .Where(h => h.HolidayDate.Year == year)
            .ToListAsync(cancellationToken);

        return Ok(EmployeeHoliday(holidays));
    }

    [HttpPost("employees")]
    public async Task<IActionResult> GetHolidayDaysForEmployees([FromBody] EmployeeHolidayRangeRequest request, CancellationToken cancellationToken)
    {
        if (request.EmployeeIds is null || request.EmployeeIds.Length == 0)
        {
            return Ok(Array.Empty<EmployeeHolidayDayDto>());
        }

        var start = request.StartDate.Date;
        var days = Math.Clamp(request.Days, 1, 366);
        var endExclusive = start.AddDays(days);

        var users = await _dashboardDbContext.Users
            .AsNoTracking()
            .Where(u => request.EmployeeIds.Contains(u.UserID))
            .Select(u => new { u.UserID, u.Initials })
            .ToListAsync(cancellationToken);

        var initialsToEmployeeId = users
            .Where(u => !string.IsNullOrWhiteSpace(u.Initials))
            .GroupBy(u => u.Initials, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.First().UserID, StringComparer.OrdinalIgnoreCase);

        if (initialsToEmployeeId.Count == 0)
        {
            return Ok(Array.Empty<EmployeeHolidayDayDto>());
        }

        var initials = initialsToEmployeeId.Keys.ToArray();

        // Pull only the date range we need; map initials -> employeeId in-memory.
        var holidayRows = await _dashboardDbContext.Holidays
            .AsNoTracking()
            .Where(h => initials.Contains(h.Initials) && h.HolidayDate >= start && h.HolidayDate < endExclusive)
            .Select(h => new { h.Initials, h.HolidayDate, h.HolidayType, h.HolidaySize })
            .ToListAsync(cancellationToken);

        var result = holidayRows
            .Where(h => initialsToEmployeeId.TryGetValue(h.Initials, out _))
            .Select(h => new EmployeeHolidayDayDto(
                EmployeeId: initialsToEmployeeId[h.Initials],
                HolidayDate: h.HolidayDate.Date,
                HolidayType: h.HolidayType,
                HolidaySize: h.HolidaySize))
            .ToList();

        return Ok(result);
    }

    [HttpPost()]
    public async Task<IActionResult> GetUpcomingHolidayForEmployees([FromBody] string[] employeeInitials)
    {
        var holidays = await _dashboardDbContext.Holidays
            .Where(holidays => employeeInitials.Any(e => e == holidays.Initials))
            .ToListAsync();

        return Ok(EmployeeHoliday(holidays));
    }

    private List<EmployeeHoliday> EmployeeHoliday(List<DashboardHoliday> holidays)
    {
        return [.. holidays.Select(h => new EmployeeHoliday
        {
            Initials = h.Initials,
            HolidayDate = DateOnly.FromDateTime(h.HolidayDate),
            HolidayType = Enum.IsDefined(typeof(HolidayType), h.HolidayType) ? (HolidayType)h.HolidayType : HolidayType.OtherReason,
            IsHalfDay = h.HolidaySize == 0.5m
        })];
    }
}
