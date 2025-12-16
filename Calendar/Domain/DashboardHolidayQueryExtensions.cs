using Calendar.Api.Domain.DashboardModels;

namespace Calendar.Api.Domain;

public static class DashboardHolidayQueryExtensions
{
    public static IQueryable<DashboardHoliday> FilterByDate(this IQueryable<DashboardHoliday> query, DateTime? startDate = null, DateTime? endDate = null)
    {
        if (startDate.HasValue)
        {
            query = query.Where(h => h.HolidayDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(h => h.HolidayDate <= endDate.Value);
        }

        return query;
    }

    public static IQueryable<DashboardHoliday> FilterByEmployeeInitials(this IQueryable<DashboardHoliday> query, List<string>? employeeInitials)
    {
        if (employeeInitials != null && employeeInitials.Any())
        {
            query = query.Where(h => employeeInitials.Contains(h.Initials));
        }
        return query;
    }
}
