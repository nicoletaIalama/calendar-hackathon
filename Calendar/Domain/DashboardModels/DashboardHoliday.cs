namespace Calendar.Api.Domain.DashboardModels;

public class DashboardHoliday
{
    public string Initials { get; set; } = default!;
    public DateOnly HolidayDate { get; set; }
    public int HolidayType { get; set; }
    public decimal HolidaySize { get; set; }
}
