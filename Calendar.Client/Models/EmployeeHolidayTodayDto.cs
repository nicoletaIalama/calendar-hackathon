namespace Calendar.Client.Models;

public sealed record EmployeeHolidayTodayDto(
    string Initials,
    DateTime HolidayDate,
    int HolidayType,
    bool IsHalfDay);
