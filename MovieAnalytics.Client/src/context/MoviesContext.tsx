// context/MoviesContext.tsx
import { createContext, useContext, useState, ReactNode } from 'react'
import { Movie } from '@/types/movie'
import { movieService } from '@/services/api/MoviesService';


interface MoviesContextType {
  movies: Movie[]
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
  loading: boolean
  error: string | null
  fetchMovies: (page: number) => Promise<void>
}

const MoviesContext = createContext<MoviesContextType | undefined>(undefined);

export function MoviesProvider({ children }: { children: ReactNode }) {
  const [movies, setMovies] = useState<Movie[]>([])
  const [currentPage, setCurrentPage] = useState(1)
  const [totalPages, setTotalPages] = useState(0)
  const [pageSize] = useState(10)
  const [totalCount, setTotalCount] = useState(0)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [hasAttemptedFetch, setHasAttemptedFetch] = useState(false)


  const fetchMovies = async (page: number) => {
    // Only fetch if we don't have movies already
    if (!hasAttemptedFetch || page !== currentPage) {
      setLoading(true)
      setError(null)
      try {
        const response = await movieService.getMovies(page, pageSize)
        setMovies(response.items)
        setCurrentPage(response.pagination.currentPage)
        setTotalPages(response.pagination.totalPages)
        setTotalCount(response.pagination.totalItems)
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to fetch movies')
      } finally {
        setLoading(false)
        setHasAttemptedFetch(true)  // Mark that we've attempted
      }
    }
  }

  const value = {
    movies,
    currentPage,
    totalPages,
    pageSize,
    totalCount,
    loading,
    error,
    fetchMovies
  }

  return (
    <MoviesContext.Provider value={value}>
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