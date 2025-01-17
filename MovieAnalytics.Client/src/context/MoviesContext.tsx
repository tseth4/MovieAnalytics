// context/MoviesContext.tsx
import { createContext, useContext, useState, ReactNode } from 'react'
import { Movie } from '@/types/movie'
import { movieService } from '@/services/api/movies';

interface MoviesContextType {
  movies: Movie[]
  loading: boolean
  error: string | null
  fetchMovies: () => Promise<void>
}

const MoviesContext = createContext<MoviesContextType | undefined>(undefined);

export function MoviesProvider({ children }: { children: ReactNode }) {
  const [movies, setMovies] = useState<Movie[]>([])
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [hasAttemptedFetch, setHasAttemptedFetch] = useState(false)  // Add this flag

  const fetchMovies = async () => {
    // Only fetch if we don't have movies already
    if (!hasAttemptedFetch) {
      setLoading(true)
      setError(null)
      try {
        const data = await movieService.getMovies()
        setMovies(data)
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to fetch movies')
      } finally {
        setLoading(false)
        setHasAttemptedFetch(true)  // Mark that we've attempted
      }
    }
  }

  return (
    <MoviesContext.Provider value={{ movies, loading, error, fetchMovies }}>
      {children}
    </MoviesContext.Provider>
  )
}

export function useMovies() {
  const context = useContext(MoviesContext)
  if (context === undefined) {
    throw new Error('useMovies must be used within a MoviesProvider')
  }
  return context
}