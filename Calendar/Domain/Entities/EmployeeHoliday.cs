using Calendar.Domain.Enums;

namespace Calendar.Api.Domain.Entities;

internal class EmployeeHoliday
{
    public string Initials { get; set; } = default!;
    public DateOnly HolidayDate { get; set; }
    public HolidayType HolidayType { get; set; }
    public bool IsHalfDay { get; set; }

}