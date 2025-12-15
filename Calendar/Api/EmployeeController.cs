using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Calendar.Domain.DashboardModels;
using Calendar.Domain.Entities;

namespace Calendar.Api;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    public EmployeeController(DashboardDbContext dashboardDbContext)
    {
        _dashboardDbContext = dashboardDbContext ?? throw new ArgumentNullException(nameof(dashboardDbContext));
    }

    private readonly DashboardDbContext _dashboardDbContext;

    [HttpGet]
    public async Task<IActionResult> GetAllEmployees([FromQuery] string? q = null)
    {
        var users = _dashboardDbContext.Users.AsNoTracking().Where(u => u.Active);

        if (!string.IsNullOrWhiteSpace(q))
        {
            // Basic search across name/email. Keep it DB-side for performance.
            var like = $"%{q}%";
            users = users.Where(u =>
                EF.Functions.Like(u.FirstName, like) ||
                EF.Functions.Like(u.Surname, like) ||
                EF.Functions.Like(u.EmailAddress, like));
        }

        var employees = await users
            .Select(u => new Employee
            {
                Id = u.UserID,
                FirstName = u.FirstName,
                LastName = u.Surname,
                Email = u.EmailAddress,
                IsActive = u.Active
            })
            .ToListAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _dashboardDbContext.Users
            .Where(e => e.UserID == id)
            .Select(u => new Employee
            {
                Id = u.UserID,
                FirstName = u.FirstName,
                LastName = u.Surname,
                Email = u.EmailAddress,
                IsActive = u.Active
            })
            .SingleOrDefaultAsync();

        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }
}
