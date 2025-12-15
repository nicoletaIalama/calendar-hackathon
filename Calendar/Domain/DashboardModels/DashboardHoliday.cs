namespace Calendar.Api.Domain.DashboardModels;

public class DashboardHoliday
{
    public string Initials { get; set; } = default!;
    public DateTime HolidayDate { get; set; }
    public int HolidayType { get; set; }
    public decimal HolidaySize { get; set; }
}
