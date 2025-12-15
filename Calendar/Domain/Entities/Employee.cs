namespace Calendar.Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = null!;
    public string Initials { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public ICollection<Holiday> Holidays { get; set; } = new List<Holiday>();
    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }
}
