param(
  [string]$LaunchProfile = "http"
)

$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$project = Join-Path $root "Calendar\Calendar.Api.csproj"

Write-Host "Starting backend (Calendar) in background..."

$proc = Start-Process `
  -FilePath "dotnet" `
  -ArgumentList @("run", "--project", $project, "--launch-profile", $LaunchProfile) `
  -WorkingDirectory $root `
  -PassThru `
  -WindowStyle Minimized

Write-Host "Backend started. PID=$($proc.Id)"
Write-Host "API base URL (http): http://localhost:5258"


