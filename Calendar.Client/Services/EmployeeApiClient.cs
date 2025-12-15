using Calendar.Client.Models;
using System.Net.Http.Json;

namespace Calendar.Client.Services;

public sealed class EmployeeApiClient(HttpClient http)
{
    public async Task<IReadOnlyList<EmployeeDto>> GetEmployeesAsync(string? query, CancellationToken cancellationToken = default)
    {
        var url = "api/Employee";
        if (!string.IsNullOrWhiteSpace(query))
        {
            url += $"?q={Uri.EscapeDataString(query)}";
        }

        var employees = await http.GetFromJsonAsync<EmployeeDto[]>(url, cancellationToken);
        return employees ?? [];
    }
}


