# MovieWatchlistAPI

ASP.NET Core Web API for managing a movie catalog and personal watchlists, with JWT authentication and role-based authorization.

## Tech Stack
- .NET 10 (`net10.0`)
- ASP.NET Core Web API
- Entity Framework Core
- ASP.NET Core Identity
- JWT Bearer Authentication
- PostgreSQL (Npgsql)

## Features
- User registration and login
- JWT token generation
- Role support (`User`, `Admin`)
- Movie CRUD endpoints
- Watchlist management per user
- Watch status updates (`PlanToWatch`, `Watching`, `Completed`, `Dropped`)
- Personal notes and movie ratings

## Project Structure
- `MovieWatchlistAPI/Controllers` - API endpoints
- `MovieWatchlistAPI/Repositories` - data access logic
- `MovieWatchlistAPI/Models` - entity models and enums
- `MovieWatchlistAPI/DTO` - request/response contracts
- `MovieWatchlistAPI/Data` - EF Core DbContext
- `MovieWatchlistAPI/Migrations` - EF Core migrations

## Prerequisites
- .NET 10 SDK
- PostgreSQL running locally (or update connection settings)

## Configuration
Edit `MovieWatchlistAPI/appsettings.json`:

- `ConnectionStrings:DefaultConnection`
- `JwtSettings:SecretKey`
- `JwtSettings:Issuer`
- `JwtSettings:Audience`
- `JwtSettings:ExpiryMinutes`

Use strong secrets in production.

## Run Locally
From repository root:

```powershell
dotnet restore
dotnet ef database update --project MovieWatchlistAPI
dotnet run --project MovieWatchlistAPI
```

Default local URLs (Development profile):
- `http://localhost:5119`
- `https://localhost:7196`

## Authentication Flow
1. Register via `POST /api/auth/register`
2. Login via `POST /api/auth/login`
3. Use returned JWT token in requests:
   - `Authorization: Bearer <token>`

## Main Endpoints
### Auth
- `POST /api/auth/register`
- `POST /api/auth/login`

### Movies
- `GET /api/movies`
- `GET /api/movies/{id}`
- `POST /api/movies` (Admin)
- `PUT /api/movies/{id}` (Admin)
- `DELETE /api/movies/{id}` (Admin)
- `PATCH /api/movies/{id}/rating`

### Watchlist
- `GET /api/watchlist`
- `POST /api/watchlist/{movieId}`
- `PATCH /api/watchlist/{movieId}/status`
- `PUT /api/watchlist/{movieId}/PersonalNote`
- `DELETE /api/watchlist/{movieId}`

## Notes
- API uses JSON enum serialization for watch status values.
- Identity roles are seeded at startup (`Admin`, `User`).
