# Calendar Hackathon

## Project structure

- **`Calendar/`**: backend (**ASP.NET Core Web API**)  
  - API endpoints live under `Calendar/Api/*Controller.cs`
  - Runs on **`http://localhost:5258`** (dev)
- **`Calendar.Client/`**: frontend (**Blazor WebAssembly**)  
  - Launches a browser page on **`http://localhost:51834`** (dev)

## Run (Windows PowerShell)

### Backend only (runs in background)

```powershell
.\run-backend.ps1
```

### Frontend only (launches browser)

```powershell
.\run-frontend.ps1
```

### Backend + Frontend

```powershell
.\run-all.ps1
```

## Notes

- The frontend reads the API base URL from `Calendar.Client/wwwroot/appsettings.json` (`ApiBaseUrl`).
- In Development, the backend enables CORS for the frontend origins:
  - `http://localhost:51834`
  - `https://localhost:51833`
