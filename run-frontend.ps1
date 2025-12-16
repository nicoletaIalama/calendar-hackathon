param(
  [string]$LaunchProfile = "https"
)

$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$project = Join-Path $root "Calendar.Client\Calendar.Client.csproj"

Write-Host "Starting frontend (Calendar.Client)..."
dotnet run --project $project --launch-profile $LaunchProfile


