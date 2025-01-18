

# MovieAnalytics 

A full-stack web application for exploring and analyzing movie data. Built with .NET Core and React.

## Features
- Browse and search through a collection of 33,000+ movies
- View detailed movie information including directors, writers, genres, and cast
- Movie posters integration with TMDB API
- Pagination and responsive design

## Tech Stack
### Backend
- .NET Core Web API
- Entity Framework Core
- SQLite Database
- Repository Pattern
- RESTful API design

### Frontend
- React with TypeScript
- Context API for state management
- Tailwind CSS & shadcn/ui for styling
- Axios for API communication

## Data Sources
- Movie dataset from IMDB
- Movie posters and additional metadata from [TMDB](https://www.themoviedb.org/)

## Getting Started

### Prerequisites
- .NET 8.0+
- Node.js
- TMDB API key

### Installation

1. Clone the repository
```bash
git clone https://github.com/yourusername/MovieAnalytics.git
```

2. Backend Setup
```bash
cd MovieAnalytics.API
dotnet restore
dotnet run
```

3. Frontend Setup
```bash
cd MovieAnalytics.Client
npm install
npm run dev
```

4. Create a `.env` file in the client directory:
```
VITE_API_URL=https://localhost:7212/api
VITE_TMDB_API_KEY=your_tmdb_api_key
```

## API Endpoints
- GET /api/movies - Get paginated list of movies
- GET /api/movies/{id} - Get specific movie details
[Add more endpoints as you implement them]



