namespace Calendar.Client.Models;

public sealed record EmployeeDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    bool IsActive)
{
    public string DisplayName => $"{FirstName} {LastName}".Trim();
}


