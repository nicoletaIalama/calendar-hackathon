using Calendar.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly Employee[] employees =
        {
            new() { Id = new Guid("35e9c1b8-ef77-4a30-be44-a2c065514116"), FirstName = "John", LastName = "Doe", Email = "John.Doe@enable.com" },
            new() { Id = new Guid("4e503abc-1000-4dd7-8f34-09bb1a023301"), FirstName = "Chris", LastName = "Flynn", Email = "chris.flynn@enable.com" },
            new(){ Id = new Guid("5940d2db-a0b1-4501-81d8-5ebd15a981c7"),FirstName = "Steven", LastName = "Birks", Email = "steven.birks@enable.com" },
            new() { Id = new Guid("a6d6a01f-d1c5-4dec-893e-f45989b323f9"), FirstName = "Nicoleta", LastName="Ialama", Email = "nicoleta.ialama@enable.com" }
        };

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
    }
}
