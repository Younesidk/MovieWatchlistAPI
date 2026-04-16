# Movie Watchlist API

## Overview
The Movie Watchlist API allows users to create, manage, and track their movie watchlists. Users can add, update, delete, and fetch movies in their watchlists seamlessly.

## Features
- **Add Movie**: Users can add new movies to their watchlist.
- **Update Movie**: Users can update details of a movie already in the watchlist.
- **Delete Movie**: Delete a movie from the watchlist.
- **Fetch Movies**: Retrieve a list of movies in the watchlist or a specific movie by ID.

## API Endpoints

### 1. Add Movie
- **Endpoint**: `POST /movies`
- **Request Body**:
  ```json
  {
      "title": "string",
      "genre": "string",
      "year": "integer",
      "watched": "boolean"
  }
  ```
- **Response**: Returns the added movie item.

### 2. Update Movie
- **Endpoint**: `PUT /movies/{id}`
- **Request Body**:
  ```json
  {
      "title": "string",
      "genre": "string",
      "year": "integer",
      "watched": "boolean"
  }
  ```
- **Response**: Returns the updated movie item.

### 3. Delete Movie
- **Endpoint**: `DELETE /movies/{id}`
- **Response**: A message confirming the deletion of the movie.

### 4. Fetch Movies
- **Endpoint**: `GET /movies`
- **Response**: Returns a list of movies in the watchlist.

### 5. Fetch Movie by ID
- **Endpoint**: `GET /movies/{id}`
- **Response**: Returns the movie with the specified ID.

## Authentication
The API uses token-based authentication. Every request must include the authentication token in the header:
```
Authorization: Bearer <token>
```

## Error Handling
The API returns standard HTTP status codes along with messages for errors:
- `200 OK`: Successful request.
- `201 Created`: Resource created successfully.
- `400 Bad Request`: Invalid request data.
- `401 Unauthorized`: Authentication failed.
- `404 Not Found`: Resource not found.
- `500 Internal Server Error`: Unexpected server error.

## Conclusion
The Movie Watchlist API provides a robust interface for managing movie watchlists, suitable for users who want to keep track of movies they want to watch, have watched, and their details. 
