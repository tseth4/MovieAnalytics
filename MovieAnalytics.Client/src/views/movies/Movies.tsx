import { useEffect } from "react"
import { MovieCard } from "./MovieCard"
import { useMovies } from "@/context/MoviesContext"


export default function Movies() {

  const { movies, loading, error, fetchMovies } = useMovies()

  // const [movies, setMovies] = useState<Movie[]>([])
  // const [loading, setLoading] = useState(true)
  // const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    console.log("fetching")
    fetchMovies()
  }, [fetchMovies])

  // const fetchMovies = async () => {
  //   try {
  //     const data = await movieService.getMovies()
  //     setMovies(data)
  //   } catch (error) {
  //     setError(error instanceof Error ? error.message : 'An error occured')
  //     console.error('Failed to fetch movies:', error)
  //   } finally {
  //     setLoading(false)
  //   }
  // }


  if (loading) return <div>Loading...</div>
  if (error) return <div>Error: {error}</div>
  return (
    <>
      <div className="container mx-auto py-8 px-4">
        <h1 className="text-3xl font-bold mb-8">Movies</h1>
        <div className="flex flex-col gap-4">
          {movies.map(movie => (
            <MovieCard key={movie.id} movie={movie} />
          ))}
        </div>
      </div>
    </>
  )
}