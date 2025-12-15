using Calendar.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Calendar.Domain.DashboardModels;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Api
{
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
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _dashboardDbContext.Users
                .Where(u => u.Active)
                .Select(u => new Employee
                {
                    Id = u.UserID,
                    FirstName = u.FirstName,
                    LastName = u.Surname,
                    Email = u.EmailAddress
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
}
