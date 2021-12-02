# War Demo

War Demo

## Getting Started

1. Install [Windows Subsytem for Linux (WSL)](https://docs.microsoft.com/en-us/windows/wsl/install-win10) - Follow manual steps
2. Install [Docker Desktop](https://docs.docker.com/desktop/)
3. Install [Git for Windows](https://git-scm.com/downloads)
4. Install [Visual Studio Professional 2022 preview](https://visualstudio.microsoft.com/vs/preview/vs2022/) - .NET 6 preview is needed

### Self-Signed Development Certificate
5. Run the following from a command prompt running as Administrator:
```
dotnet dev-certs https -ep %APPDATA%\ASP.NET\https\aspnetapp.pfx -p !Password123
dotnet dev-certs https --trust
```

PowerShell as Administrator:
```
dotnet dev-certs https -ep $env:appdata\ASP.NET\https\aspnetapp.pfx -p !Password123
dotnet dev-certs https --trust
```

### Docker Compose
6. The easiest way to get up-and-running is to use Docker Compose. From a git bash window, PowerShell, or command prompt (as administrator) in the root directory run:
```
docker compose up -d
```

### Database
7. Before testing the endpoints, we need to create the database. In Visual Studio open a Package Manager Console and execute:
```
update-database
```

### Swagger
8. Open a web browser and navigate the the url:

https://localhost:44398/swagger/index.html

