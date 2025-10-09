cd backend
dotnet restore
dotnet ef database update --project src/SchoolApp.Infrastructure
dotnet run --project src/SchoolApp.Api