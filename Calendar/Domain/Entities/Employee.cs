namespace Calendar.Domain.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public ICollection<Holiday> Holidays { get; set; } = new List<Holiday>();
    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }
}
