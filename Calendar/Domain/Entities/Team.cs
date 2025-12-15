namespace Calendar.Domain.Entities;

public class Team
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Employee> Members { get; set; } = new List<Employee>();
}
