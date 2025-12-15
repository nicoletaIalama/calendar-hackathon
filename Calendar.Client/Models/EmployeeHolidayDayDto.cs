namespace Calendar.Client.Models;

public sealed record EmployeeHolidayDayDto(
    int EmployeeId,
    DateTime HolidayDate,
    int HolidayType,
    decimal HolidaySize);


