import { useEffect, useState } from "react"

export interface Movie {
  id: string
  title: string
  year: number
  rating: number
  directorNames: string[]
  writerNames: string[]
  starNames: string[]
  genreNames: string[]
}


export default function Movies() {
  const [movies, setMovies] = useState<Movie[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    fetchMovies()
  }, [])

  const fetchMovies = async () => {
    try {
      const response = await fetch('https://localhost:7212/api/Movies')
      if (!response.ok) throw new Error('Failed to fetch movies')
      const data = await response.json()

      setMovies(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occured')
    } finally {
      setLoading(false)
    }
  }
  if (loading) return <div>Loading...</div>
  if (error) return <div>Error: {error}</div>
  return (
    <>

      {movies.map(movie => (
        <div key={movie.id}>
          <h2>{movie.title}</h2>
          <p>Year: {movie.year}</p>
          <p>Rating: {movie.rating}</p>
          <p>Directors: {movie.directorNames.join(', ')}</p>
          <p>Genres: {movie.genreNames.join(', ')}</p>
          <p>Stars: {movie.starNames.join(', ')}</p>
          <p>Writers: {movie.writerNames.join(', ')}</p>
        </div>
      ))}
    </>
  )
}