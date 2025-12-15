namespace Calendar.Domain.Entities;

public class CalendarView
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public List<Guid> EmployeeIds { get; set; } = new();

    public Guid? OwnerUserId { get; set; }
}
