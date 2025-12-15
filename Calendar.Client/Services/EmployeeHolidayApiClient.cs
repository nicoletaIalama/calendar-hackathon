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

}
