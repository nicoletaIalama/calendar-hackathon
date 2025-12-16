param(
  [string]$BackendLaunchProfile = "https",
  [string]$FrontendLaunchProfile = "https",
  [int]$BackendWarmupSeconds = 2
)

$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $MyInvocation.MyCommand.Path

& (Join-Path $root "run-backend.ps1") -LaunchProfile $BackendLaunchProfile

Start-Sleep -Seconds $BackendWarmupSeconds

& (Join-Path $root "run-frontend.ps1") -LaunchProfile $FrontendLaunchProfile


