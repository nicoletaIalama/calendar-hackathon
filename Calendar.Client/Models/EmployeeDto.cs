namespace Calendar.Client.Models;

public sealed record EmployeeDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    bool IsActive)
{
    public string DisplayName => $"{FirstName} {LastName}".Trim();
}


