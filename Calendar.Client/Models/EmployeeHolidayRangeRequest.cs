namespace Calendar.Client.Models;

public sealed record EmployeeHolidayRangeRequest(
    int[] EmployeeIds,
    DateTime StartDate,
    int Days);


