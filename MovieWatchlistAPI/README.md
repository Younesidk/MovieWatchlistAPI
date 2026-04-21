# 🎬 MovieWatchlistAPI

> A RESTful API for managing your personal movie watchlist — built with ASP.NET Core, EF Core, and JWT authentication.

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Database-336791?logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![JWT](https://img.shields.io/badge/Auth-JWT-000000?logo=jsonwebtokens&logoColor=white)](https://jwt.io/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen)](https://github.com/Younesidk/MovieWatchlistAPI)

---

## 📖 About the Project

MovieWatchlistAPI is a backend REST API that allows users to manage a personal movie watchlist. You can browse and manage a catalog of movies, add them to your personal watchlist, rate them, and attach private notes — all secured with JWT-based authentication.

This project was built as a hands-on learning exercise to practice building production-style APIs with **ASP.NET Core**, **Entity Framework Core**, and **JWT authentication**. It follows clean architectural patterns and provides a solid foundation that can be extended into a full-stack application.

---

## ✨ Features

- 🔐 **User Registration & Authentication** — Secure sign-up and login with JWT Bearer tokens
- 🎬 **Movie Management** — Full CRUD operations for the movie catalog
- 📋 **Personal Watchlist** — Add or remove movies from your own watchlist
- ⭐ **Rating System** — Rate movies on a 1–5 scale and view average ratings
- 📝 **Private Notes** — Attach personal notes to any movie for your own reference
- 🛡️ **Role-Based Security** — Endpoints protected with `[Authorize]` attributes
- 📄 **Swagger/OpenAPI** — Interactive API documentation out of the box
- 🗃️ **PostgreSQL** — Robust relational database via EF Core with code-first migrations

---

## 🛠️ Tech Stack

| Technology | Purpose |
|---|---|
| **ASP.NET Core (.NET 10)** | Web API framework — handles routing, middleware, and dependency injection |
| **Entity Framework Core** | ORM — manages database access, migrations, and model relationships |
| **PostgreSQL** | Relational database — stores all persistent data |
| **JWT (JSON Web Tokens)** | Authentication — stateless, token-based user verification |
| **Swagger / OpenAPI** | API documentation — auto-generated interactive docs at `/swagger` |
| **BCrypt** | Password hashing — securely stores user passwords |

---

## 🚀 Getting Started

Follow these steps to get the API running on your local machine.

### Prerequisites

Make sure you have the following installed:

- [**.NET 10 SDK**](https://dotnet.microsoft.com/download) — or the latest available SDK
- [**PostgreSQL**](https://www.postgresql.org/download/) — running locally or via Docker
- A REST client like [Postman](https://www.postman.com/), [Insomnia](https://insomnia.rest/), or `curl`

### 1. Clone the Repository

```bash
git clone https://github.com/Younesidk/MovieWatchlistAPI.git
cd MovieWatchlistAPI
```

### 2. Configure the Database & JWT

Open `appsettings.json` and update the following values to match your environment:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=moviewatchlist;Username=your_pg_user;Password=your_pg_password"
  },
  "Jwt": {
    "Key": "your_super_secret_key_at_least_32_characters_long",
    "Issuer": "MovieWatchlistAPI",
    "Audience": "MovieWatchlistAPI",
    "ExpireMinutes": 60
  }
}
```

> ⚠️ **Security Note:** Never commit real secrets to version control. For production, use **User Secrets**, **environment variables**, or a secrets manager.

### 3. Apply EF Core Migrations

Create the database and apply all pending migrations:

```bash
# Install EF Core tools if not already installed
dotnet tool install --global dotnet-ef

# Apply migrations (this also creates the database if it doesn't exist)
dotnet ef database update
```

> If you need to create a new migration after model changes:
> ```bash
> dotnet ef migrations add YourMigrationName
> dotnet ef database update
> ```

### 4. Run the Project

```bash
dotnet run
```

By default, the API will be available at:

- **HTTP:** `http://localhost:5000`
- **Swagger UI:** `http://localhost:5000/swagger`

---

## 📡 API Endpoints Documentation

All endpoints are prefixed with `/api`. Protected routes require a valid JWT Bearer token in the `Authorization` header.

---

### 🔐 Authentication

| Method | Route | Auth Required | Description |
|---|---|---|---|
| `POST` | `/api/auth/register` | ❌ No | Register a new user account |
| `POST` | `/api/auth/login` | ❌ No | Log in and receive a JWT token |

#### `POST /api/auth/register`

Register a new user.

**Request Body:**

```json
{
  "username": "moviefan42",
  "email": "moviefan42@example.com",
  "password": "SecureP@ssw0rd!"
}
```

**Success Response — `200 OK`:**

```json
{
  "message": "User registered successfully",
  "username": "moviefan42"
}
```

**Error Responses:**

| Status Code | Meaning |
|---|---|
| `400 Bad Request` | Validation error (missing fields, invalid email, etc.) |
| `409 Conflict` | Username or email already exists |

---

#### `POST /api/auth/login`

Authenticate and receive a JWT token.

**Request Body:**

```json
{
  "username": "moviefan42",
  "password": "SecureP@ssw0rd!"
}
```

**Success Response — `200 OK`:**

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "moviefan42",
  "expiresAt": "2025-07-18T14:30:00Z"
}
```

**Error Responses:**

| Status Code | Meaning |
|---|---|
| `400 Bad Request` | Missing or malformed fields |
| `401 Unauthorized` | Invalid username or password |

---

### 🎬 Movies

| Method | Route | Auth Required | Description |
|---|---|---|---|
| `GET` | `/api/movies` | ❌ No | Get all movies (with optional filtering) |
| `GET` | `/api/movies/{id}` | ❌ No | Get a single movie by ID |
| `POST` | `/api/movies` | ✅ Yes | Add a new movie |
| `PUT` | `/api/movies/{id}` | ✅ Yes | Update an existing movie |
| `DELETE` | `/api/movies/{id}` | ✅ Yes | Delete a movie |

#### `GET /api/movies`

Retrieve a list of all movies.

**Success Response — `200 OK`:**

```json
[
  {
    "id": 1,
    "title": "Inception",
    "genre": "Sci-Fi",
    "releaseYear": 2010,
    "director": "Christopher Nolan",
    "description": "A thief who steals corporate secrets through the use of dream-sharing technology.",
    "averageRating": 4.5
  },
  {
    "id": 2,
    "title": "The Grand Budapest Hotel",
    "genre": "Comedy",
    "releaseYear": 2014,
    "director": "Wes Anderson",
    "description": "A writer encounters the owner of an aging high-class hotel.",
    "averageRating": 4.2
  }
]
```

---

#### `GET /api/movies/{id}`

Retrieve a single movie by its ID.

**Success Response — `200 OK`:**

```json
{
  "id": 1,
  "title": "Inception",
  "genre": "Sci-Fi",
  "releaseYear": 2010,
  "director": "Christopher Nolan",
  "description": "A thief who steals corporate secrets through the use of dream-sharing technology.",
  "averageRating": 4.5
}
```

**Error Responses:**

| Status Code | Meaning |
|---|---|
| `404 Not Found` | Movie with the given ID does not exist |

---

#### `POST /api/movies`

Add a new movie to the catalog. **Requires authentication.**

**Request Body:**

```json
{
  "title": "Interstellar",
  "genre": "Sci-Fi",
  "releaseYear": 2014,
  "director": "Christopher Nolan",
  "description": "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival."
}
```

**Success Response — `201 Created`:**

```json
{
  "id": 3,
  "title": "Interstellar",
  "genre": "Sci-Fi",
  "releaseYear": 2014,
  "director": "Christopher Nolan",
  "description": "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
  "averageRating": null
}
```

**Error Responses:**

| Status Code | Meaning |
|---|---|
| `400 Bad Request` | Validation error (missing required fields) |
| `401 Unauthorized` | Missing or invalid JWT token |

---

#### `PUT /api/movies/{id}`

Update an existing movie. **Requires authentication.**

**Request Body:**

```json
{
  "title": "Interstellar",
  "genre": "Sci-Fi / Adventure",
  "releaseYear": 2014,
  "director": "Christopher Nolan",
  "description": "Updated description here."
}
```

**Success Response — `200 OK`:**

```json
{
  "id": 3,
  "title": "Interstellar",
  "genre": "Sci-Fi / Adventure",
  "releaseYear": 2014,
  "director": "Christopher Nolan",
  "description": "Updated description here.",
  "averageRating": null
}
```

**Error Responses:**

| Status Code | Meaning |
|---|---|
| `400 Bad Request` | Validation error |
| `401 Unauthorized` | Missing or invalid JWT token |
| `404 Not Found` | Movie with the given ID does not exist |

---

#### `DELETE /api/movies/{id}`

Delete a movie from the catalog. **Requires authentication.**

**Success Response — `200 OK`:**

```json
{
  "message": "Movie deleted successfully"
}
```

**Error Responses:**

| Status Code | Meaning |
|---|---|
| `401 Unauthorized` | Missing or invalid JWT token |
| `404 Not Found` | Movie with the given ID does not exist |

---

### 📋 Watchlist

| Method | Route | Auth Required | Description |
|---|---|---|---|
| `GET` | `/api/watchlist` | ✅ Yes | Get the current user's watchlist |
| `POST` | `/api/watchlist/{movieId}` | ✅ Yes | Add a movie to the watchlist |
| `DELETE` | `/api/watchlist/{movieId}` | ✅ Yes | Remove a movie from the watchlist |

#### `GET /api/watchlist`

Retrieve the authenticated user's personal watchlist. **Requires authentication.**

**Success Response — `200 OK`:**

```json
[
  {
    "id": 1,
    "movieId": 1,
    "title": "Inception",
    "genre": "Sci-Fi",
    "releaseYear": 2010,
    "director": "Christopher Nolan",
    "addedAt": "2025-07-17T10:30:00Z"
  },
  {
    "id": 2,
    "movieId": 3,
    "title": "Interstellar",
    "genre": "Sci-Fi",
    "releaseYear": 2014,
    "director": "Christopher Nolan",
    "addedAt": "2025-07-17T11:00:00Z"
  }
]
```

---

#### `POST /api/watchlist/{movieId}`

Add a movie to the authenticated user's watchlist. **Requires authentication.**

**Success Response — `200 OK`:**

```json
{
  "message": "Movie added to watchlist",
  "movieId": 5
}
```

**Error Responses:**

| Status Code | Meaning |
|---|---|
| `401 Unauthorized` | Missing or invalid JWT token |
| `404 Not Found` | Movie with the given ID does not exist |
| `409 Conflict` | Movie is already in the user's watchlist |

---

#### `DELETE /api/watchlist/{movieId}`

Remove a movie from the authenticated user's watchlist. **Requires authentication.**

**Success Response — `200 OK`:**

```json
{
  "message": "Movie removed from watchlist",
  "movieId": 5
}
```

**Error Responses:**

| Status Code | Meaning |
|---|---|
| `401 Unauthorized` | Missing or invalid JWT token |
| `404 Not Found` | Movie is not in the user's watchlist |

---

### ⭐ Ratings

| Method | Route | Auth Required | Description |
|---|---|---|---|
| `GET` | `/api/movies/{movieId}/ratings` | ❌ No | Get all ratings for a movie |
| `POST` | `/api/movies/{movieId}/ratings` | ✅ Yes | Rate a movie (or update your existing rating) |
| `DELETE` | `/api/ratings/{ratingId}` | ✅ Yes | Delete a rating |

#### `GET /api/movies/{movieId}/ratings`

Retrieve all ratings for a specific movie.

**Success Response — `200 OK`:**

```json
[
  {
    "id": 1,
    "movieId": 1,
    "username": "moviefan42",
    "score": 5,
    "createdAt": "2025-07-15T09:00:00Z"
  },
  {
    "id": 2,
    "movieId": 1,
    "username": "cinemalover",
    "score": 4,
    "createdAt": "2025-07-16T14:20:00Z"
  }
]
```

**Error Responses:**

| Status Code | Meaning |
|---|---|
| `404 Not Found` | Movie with the given ID does not exist |

---

#### `POST /api/movies/{movieId}/ratings`

Submit a rating (1-5) for a movie. If the user has already rated this movie, the existing rating is updated. **Requires authentication.**

**Request Body:**

```json
{
  "score": 4
}
```

**Success Response — `200 OK`:**

```json
{
  "id": 3,
  "movieId": 1,
  "username": "moviefan42",
  "score": 4,
  "createdAt": "2025-07-17T10:00:00Z"
}
```

**Error Responses:**

| Status Code | Meaning |
|---|---|
| `400 Bad Request` | Score out of range (must be 1-5) or missing |
| `401 Unauthorized` | Missing or invalid JWT token |
| `404 Not Found` | Movie with the given ID does not exist |

---

#### `DELETE /api/ratings/{ratingId}`

Delete a rating you previously submitted. **Requires authentication.**

**Success Response — `200 OK`:**

```json
{
  "message": "Rating deleted successfully"
}
```

**Error Responses:**

| Status Code | Meaning |
|---|---|
| `401 Unauthorized` | Missing or invalid JWT token |
| `403 Forbidden` | You are not the owner of this rating |
| `404 Not Found` | Rating with the given ID does not exist |

---

### 📝 Notes

| Method | Route | Auth Required | Description |
|---|---|---|---|
| `GET` | `/api/notes` | ✅ Yes | Get all notes for the current user |
| `GET` | `/api/notes/{movieId}` | ✅ Yes | Get the current user's note for a specific movie |
| `POST` | `/api/notes/{movieId}` | ✅ Yes | Create or update a note on a movie |
| `DELETE` | `/api/notes/{noteId}` | ✅ Yes | Delete a note |

#### `GET /api/notes`

Retrieve all notes belonging to the authenticated user. **Requires authentication.**

**Success Response — `200 OK`:**

```json
[
  {
    "id": 1,
    "movieId": 1,
    "movieTitle": "Inception",
    "content": "Must watch the ending again — so many layers!",
    "createdAt": "2025-07-15T08:00:00Z",
    "updatedAt": "2025-07-16T09:30:00Z"
  },
  {
    "id": 2,
    "movieId": 3,
    "movieTitle": "Interstellar",
    "content": "Remind Alex about the docking scene.",
    "createdAt": "2025-07-17T11:05:00Z",
    "updatedAt": "2025-07-17T11:05:00Z"
  }
]
```

---

#### `GET /api/notes/{movieId}`

Retrieve the authenticated user's note for a specific movie. **Requires authentication.**

**Success Response — `200 OK`:**

```json
{
  "id": 1,
  "movieId": 1,
  "movieTitle": "Inception",
  "content": "Must watch the ending again — so many layers!",
  "createdAt": "2025-07-15T08:00:00Z",
  "updatedAt": "2025-07-16T09:30:00Z"
}
```

**Error Responses:**

| Status Code | Meaning |
|---|---|
| `401 Unauthorized` | Missing or invalid JWT token |
| `404 Not Found` | No note exists for this user/movie combination |

---

#### `POST /api/notes/{movieId}`

Create a new note or update the existing note on a movie. **Requires authentication.**

**Request Body:**

```json
{
  "content": "Watch the director's commentary after the first viewing."
}
```

**Success Response — `200 OK`:**

```json
{
  "id": 3,
  "movieId": 2,
  "movieTitle": "The Grand Budapest Hotel",
  "content": "Watch the director's commentary after the first viewing.",
  "createdAt": "2025-07-17T12:00:00Z",
  "updatedAt": "2025-07-17T12:00:00Z"
}
```

**Error Responses:**

| Status Code | Meaning |
|---|---|
| `400 Bad Request` | Content is empty or missing |
| `401 Unauthorized` | Missing or invalid JWT token |
| `404 Not Found` | Movie with the given ID does not exist |

---

#### `DELETE /api/notes/{noteId}`

Delete a note you previously created. **Requires authentication.**

**Success Response — `200 OK`:**

```json
{
  "message": "Note deleted successfully"
}
```

**Error Responses:**

| Status Code | Meaning |
|---|---|
| `401 Unauthorized` | Missing or invalid JWT token |
| `403 Forbidden` | You are not the owner of this note |
| `404 Not Found` | Note with the given ID does not exist |

---

## 🔑 Authentication Guide

This API uses **JWT (JSON Web Tokens)** for stateless authentication. Here's how it works:

### Step 1 — Register

Send a `POST` request to `/api/auth/register` with your desired credentials.

```bash
curl -X POST https://localhost:5000/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"username":"moviefan42","email":"moviefan42@example.com","password":"SecureP@ssw0rd!"}'
```

### Step 2 — Log In

Send a `POST` request to `/api/auth/login` with your credentials. The API responds with a JWT token.

```bash
curl -X POST https://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"moviefan42","password":"SecureP@ssw0rd!"}'
```

### Step 3 — Use the Token

Include the token in the `Authorization` header of subsequent requests using the `Bearer` scheme:

```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Example:**

```bash
curl -X GET https://localhost:5000/api/watchlist \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

> 💡 **Tip:** In Swagger UI, click the 🔒 **Authorize** button and paste your token (without the `Bearer` prefix — Swagger adds it automatically) to authenticate all requests in the session.

### Token Details

- Tokens are signed with an HMAC-SHA256 secret configured in `appsettings.json`
- Default expiration is **60 minutes** (configurable)
- Tokens contain the user's ID and username as claims
- Expired or tampered tokens will result in a `401 Unauthorized` response

---

## 🗃️ Database Schema / Models Overview

The API uses five main entities with the following relationships:

```
┌──────────┐       ┌──────────────────┐       ┌──────────┐
│   User   │───────│  WatchlistItem   │───────│  Movie   │
│          │  1:N  │                  │  N:1   │          │
└──────────┘       └──────────────────┘       └──────────┘
     │                                              │
     │ 1:N                                     N:1  │
     ▼                                              ▼
┌──────────┐                                 ┌──────────┐
│  Rating  │─────────────────────────────────│          │
│          │            N:1                  │          │
└──────────┘                                 └──────────┘
     │
     │ 1:N
     ▼
┌──────────┐
│   Note   │
│          │
└──────────┘
```

### Entity Breakdown

| Model | Description | Key Fields |
|---|---|---|
| **User** | Represents a registered user. | `Id`, `Username`, `Email`, `PasswordHash` |
| **Movie** | A movie in the catalog. Any authenticated user can add movies. | `Id`, `Title`, `Genre`, `ReleaseYear`, `Director`, `Description` |
| **WatchlistItem** | A join entity linking a User to a Movie they want to watch. One per user per movie. | `Id`, `UserId`, `MovieId`, `AddedAt` |
| **Rating** | A user's rating (1–5) for a movie. One rating per user per movie. | `Id`, `UserId`, `MovieId`, `Score`, `CreatedAt` |
| **Note** | A private note attached by a user to a movie. One note per user per movie. | `Id`, `UserId`, `MovieId`, `Content`, `CreatedAt`, `UpdatedAt` |

### Relationships

- A **User** can have many **WatchlistItems**, **Ratings**, and **Notes** (one-to-many)
- A **Movie** can appear in many **WatchlistItems**, **Ratings**, and **Notes** (one-to-many)
- **WatchlistItem**, **Rating**, and **Note** each have a composite uniqueness constraint on `(UserId, MovieId)` — a user can only have one of each per movie

---

## 📁 Project Structure

```
MovieWatchlistAPI/
├── Controllers/            # API endpoint controllers
│   ├── AuthController.cs       # Registration & login endpoints
│   ├── MoviesController.cs     # Movie CRUD endpoints
│   ├── WatchlistController.cs  # Watchlist management endpoints
│   ├── RatingsController.cs    # Rating endpoints
│   └── NotesController.cs      # Notes endpoints
├── Models/                 # Entity / domain models
│   ├── User.cs                 # User entity
│   ├── Movie.cs                # Movie entity
│   ├── WatchlistItem.cs        # Watchlist join entity
│   ├── Rating.cs               # Rating entity
│   └── Note.cs                 # Note entity
├── DTOs/                   # Data Transfer Objects (request/response shapes)
│   ├── AuthDtos.cs             # RegisterDto, LoginDto, AuthResponseDto
│   ├── MovieDtos.cs            # MovieDto, CreateMovieDto, UpdateMovieDto
│   ├── WatchlistDtos.cs        # WatchlistResponseDto
│   ├── RatingDtos.cs           # RatingDto, CreateRatingDto
│   └── NoteDtos.cs             # NoteDto, CreateNoteDto
├── Data/                   # EF Core database context & configuration
│   └── AppDbContext.cs         # DbContext with DbSets and model configuration
├── Migrations/             # Auto-generated EF Core migration files
├── Services/               # Business logic & helper services
│   └── JwtService.cs           # JWT token generation & validation
├── Program.cs              # Application entry point & service configuration
├── appsettings.json        # Configuration (connection strings, JWT settings)
└── MovieWatchlistAPI.csproj  # Project file with NuGet package references
```

---

## 🤝 Contributing

This is a personal learning project, but I welcome contributions, suggestions, and feedback of any kind! Whether it's a bug fix, a new feature idea, or improved documentation — feel free to jump in.

### How to Contribute

1. **Fork** the repository
2. **Create a feature branch**: `git checkout -b feature/your-feature-name`
3. **Commit your changes**: `git commit -m "Add your descriptive message"`
4. **Push to your branch**: `git push origin feature/your-feature-name`
5. **Open a Pull Request** with a clear description of your changes

### Guidelines

- Keep PRs focused — one feature or fix per PR
- Follow the existing code style and naming conventions
- If adding a new endpoint, please update this README accordingly

---

## 📄 License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

---

## 👤 Author & Acknowledgements

**Built by [Younesidk](https://github.com/Younesidk)**

This project was created as a hands-on exercise to deepen my understanding of backend development with ASP.NET Core. If you find it useful or learn something from it, that's a win!

### Acknowledgements

- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/) — for the excellent official docs
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) — for making database access feel effortless
- [JWT.io](https://jwt.io/) — for the handy token debugging tool
- The open-source community — for all the packages and resources that make projects like this possible

---

<p align="center">
  Made with ☕ and curiosity
</p>

