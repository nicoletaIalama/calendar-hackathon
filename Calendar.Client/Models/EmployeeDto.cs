namespace Calendar.Client.Models;

public sealed record EmployeeDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    bool IsActive,
    string Initials)
{
    public string DisplayName => $"{FirstName} {LastName}".Trim();
}


