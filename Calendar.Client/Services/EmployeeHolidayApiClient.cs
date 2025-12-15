using Calendar.Client.Models;
using System.Net.Http.Json;

namespace Calendar.Client.Services;

public sealed class EmployeeHolidayApiClient(HttpClient http)
{
    public async Task<List<HolidayDto>> GetEmployeeHolidayAsync(string initials, CancellationToken cancellationToken = default)
    {
        var url = "api/Holiday/employee";
        if (!string.IsNullOrWhiteSpace(initials))
        {
            url += $"/{Uri.EscapeDataString(initials)}";
        }

        var employees = await http.GetFromJsonAsync<HolidayDto[]>(url, cancellationToken);
        return employees.ToList() ?? new List<HolidayDto>();
    }

    public async Task<IReadOnlyList<EmployeeHolidayDayDto>> GetEmployeesHolidayDaysAsync(
        int[] employeeIds,
        DateTime startDate,
        int days,
        CancellationToken cancellationToken = default)
    {
        var request = new EmployeeHolidayRangeRequest(employeeIds, startDate.Date, days);

        var response = await http.PostAsJsonAsync("api/Holiday/employees", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var holidays = await response.Content.ReadFromJsonAsync<EmployeeHolidayDayDto[]>(cancellationToken: cancellationToken);
        return holidays ?? Array.Empty<EmployeeHolidayDayDto>();
    }

}
